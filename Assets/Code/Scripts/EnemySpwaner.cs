using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemySpawner : MonoBehaviour
{

    [Header("Referneces")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 4;
    [SerializeField] private float enimiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private int maxWaves = 3; // 총 3 웨이브
    
    [Header("Spawn Settings")]
    [SerializeField] private int enemiesPerSpawn = 1; // 한 번에 스폰할 적의 수
    [SerializeField] private bool enableDebugLogs = true; // 디버그 로그 활성화

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    
    // Round UI를 위한 이벤트
    public delegate void OnRoundInfoChanged(int currentRound, int maxRounds, int totalEnemies, int remainingEnemies, int enemiesInField);
    public static event OnRoundInfoChanged onRoundInfoChanged;

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int totalEnemiesForThisWave; // 이번 웨이브 총 적 수
    private int enemiesSpawnedThisWave; // 이번 웨이브에서 스폰된 적 수
    private bool isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        UpdateRoundUI(); // 초기 라운드 정보 업데이트
        StartCoroutine(StartWave());
    }
    
    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        // 스폰 간격 체크 및 남은 적이 있는지 확인
        if (timeSinceLastSpawn >= (1f / enimiesPerSecond) && GetRemainingEnemies() > 0)
        {
            SpawnEnemy();
        }

        // 웨이브 종료 조건: 모든 적이 스폰되고 필드에 적이 없을 때
        if (GetRemainingEnemies() <= 0 && CountEnemiesInScene() <= 0)
        {
            EndWave();
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
        UpdateRoundUI(); // UI 업데이트
        
        if (enableDebugLogs)
        {
            Debug.Log($"[EnemySpawner] Enemy destroyed. Enemies alive: {enemiesAlive}, In scene: {CountEnemiesInScene()}");
        }
    }
    
    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        isSpawning = true;
        totalEnemiesForThisWave = EnemiesPerWave();
        enemiesSpawnedThisWave = 0;
        
        if (enableDebugLogs)
        {
            Debug.Log($"[EnemySpawner] Starting Wave {currentWave} with {totalEnemiesForThisWave} enemies");
        }
        
        UpdateRoundUI(); // 웨이브 시작시 UI 업데이트
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        
        if (enableDebugLogs)
        {
            Debug.Log($"[EnemySpawner] Wave {currentWave} completed. Spawned: {enemiesSpawnedThisWave}/{totalEnemiesForThisWave}");
        }
        
        currentWave++;
        
        // 모든 웨이브를 클리어했는지 확인
        if (currentWave > maxWaves)
        {
            // 게임 클리어!
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Victory();
            }
        }
        else
        {
            StartCoroutine(StartWave());
        }
    }
    
    private void SpawnEnemy()
    {
        int remainingEnemies = GetRemainingEnemies();
        int enemiesToSpawn = Mathf.Min(enemiesPerSpawn, remainingEnemies);
        
        if (enemiesToSpawn <= 0) return;
        
        int successfulSpawns = 0;
        int maxRetries = 3; // 최대 재시도 횟수
        
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            bool spawned = false;
            int retryCount = 0;
            
            // 스폰이 성공할 때까지 재시도
            while (!spawned && retryCount < maxRetries)
            {
                if (SpawnSingleEnemy())
                {
                    spawned = true;
                    successfulSpawns++;
                    enemiesSpawnedThisWave++;
                    enemiesAlive++;
                }
                else
                {
                    retryCount++;
                    if (enableDebugLogs && retryCount > 1)
                    {
                        Debug.LogWarning($"[EnemySpawner] Spawn retry {retryCount}/{maxRetries}");
                    }
                }
            }
            
            if (!spawned)
            {
                Debug.LogError($"[EnemySpawner] Failed to spawn enemy after {maxRetries} retries!");
            }
        }
        
        if (enableDebugLogs)
        {
            Debug.Log($"[EnemySpawner] Attempted to spawn {enemiesToSpawn}, successful: {successfulSpawns}. Total spawned this wave: {enemiesSpawnedThisWave}/{totalEnemiesForThisWave}");
        }
        
        timeSinceLastSpawn = 0f;
        UpdateRoundUI(); // 적 생성시 UI 업데이트
    }
    
    private bool SpawnSingleEnemy()
    {
        try
        {
            if (enemyPrefabs == null || enemyPrefabs.Length == 0)
            {
                Debug.LogError("[EnemySpawner] No enemy prefabs assigned!");
                return false;
            }
            
            if (LevelManager.main == null || LevelManager.main.startPoint == null)
            {
                Debug.LogError("[EnemySpawner] LevelManager or startPoint is null!");
                return false;
            }
            
            int index = Random.Range(0, enemyPrefabs.Length);
            GameObject prefabToSpawn = enemyPrefabs[index];
            
            if (prefabToSpawn == null)
            {
                Debug.LogError($"[EnemySpawner] Enemy prefab at index {index} is null!");
                return false;
            }
            
            // 스폰 위치에 약간의 랜덤 오프셋 추가 (동시 스폰 시 겹침 방지)
            Vector3 basePosition = LevelManager.main.startPoint.position;
            Vector3 spawnPosition = basePosition + new Vector3(
                Random.Range(-0.2f, 0.2f), // X축 랜덤 오프셋
                Random.Range(-0.2f, 0.2f), // Y축 랜덤 오프셋
                0f // Z축은 고정
            );
            
            GameObject spawnedEnemy = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            
            if (spawnedEnemy != null)
            {
                // Enemy 태그가 설정되지 않았다면 설정
                if (spawnedEnemy.tag != "Enemy")
                {
                    spawnedEnemy.tag = "Enemy";
                }
                
                // 전체 Z 좌표를 0으로 고정 (2D 게임에서 깊이 문제 방지)
                Vector3 fixedPos = spawnedEnemy.transform.position;
                fixedPos.z = 0f;
                spawnedEnemy.transform.position = fixedPos;
                
                if (enableDebugLogs)
                {
                    Debug.Log($"[EnemySpawner] Successfully spawned enemy at position: {spawnedEnemy.transform.position}");
                }
                
                return true;
            }
            else
            {
                Debug.LogError("[EnemySpawner] Failed to instantiate enemy prefab!");
                return false;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[EnemySpawner] Exception during enemy spawn: {e.Message}");
            return false;
        }
    }

    private int EnemiesPerWave()
    {
        int enemies = Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
        
        if (enableDebugLogs)
        {
            Debug.Log($"[EnemySpawner] Wave {currentWave} calculated to have {enemies} enemies (base: {baseEnemies}, factor: {difficultyScalingFactor})");
        }
        
        return enemies;
    }
    
    // 남은 적의 수 계산 (실제 적의 수)
    private int GetRemainingEnemies()
    {
        return totalEnemiesForThisWave - enemiesSpawnedThisWave;
    }
    
    // Round UI 업데이트 메서드
    private void UpdateRoundUI()
    {
        // 실제 씬에 있는 적의 수를 직접 계산
        int actualEnemiesInField = CountEnemiesInScene();
        int remainingEnemies = GetRemainingEnemies();
        int totalEnemies = totalEnemiesForThisWave;
        
        onRoundInfoChanged?.Invoke(currentWave, maxWaves, totalEnemies, remainingEnemies, actualEnemiesInField);
        
        if (enableDebugLogs)
        {
            Debug.Log($"[EnemySpawner] UI Update - Round: {currentWave}/{maxWaves}, Total: {totalEnemies}, Remaining: {remainingEnemies}, In Field: {actualEnemiesInField} (internal: {enemiesAlive})");
        }
    }
    
    // 실제 씬에 있는 적의 수를 계산하는 메서드
    private int CountEnemiesInScene()
    {
        try
        {
            // "Enemy" 태그를 가진 오브젝트의 수를 세기
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies != null && enemies.Length > 0)
            {
                return enemies.Length;
            }
            
            // 태그가 설정되지 않은 경우 Enemy 컴포넌트로 찾기
            Enemy[] enemyComponents = FindObjectsOfType<Enemy>();
            return enemyComponents != null ? enemyComponents.Length : 0;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[EnemySpawner] Error counting enemies: {e.Message}");
            return 0;
        }
    }
    
    // 디버그용 메서드
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying && isSpawning)
        {
            // 스폰 정보를 Scene 뷰에 표시
            UnityEditor.Handles.Label(transform.position + Vector3.up * 2, 
                $"Wave: {currentWave}/{maxWaves}\nTotal: {totalEnemiesForThisWave}\nSpawned: {enemiesSpawnedThisWave}\nRemaining: {GetRemainingEnemies()}\nIn Field: {CountEnemiesInScene()}");
        }
    }
    
    // 디버그 정보 출력 메서드
    public void PrintDebugInfo()
    {
        Debug.Log($"[EnemySpawner Debug] Wave: {currentWave}/{maxWaves}, Total: {totalEnemiesForThisWave}, Spawned: {enemiesSpawnedThisWave}, Remaining: {GetRemainingEnemies()}, In Field: {CountEnemiesInScene()}, Internal Alive: {enemiesAlive}");
    }
}
