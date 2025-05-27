using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static GameManager Instance;

    // 게임 상태 열거형
    public enum GameState
    {
        Playing,    // 게임 진행중
        Paused,     // 일시정지
        GameOver,   // 게임 오버
        Victory     // 게임 클리어
    }

    // 현재 게임 상태
    private GameState currentState = GameState.Playing;

    // 플레이어 체력
    [Header("Player Settings")]
    [SerializeField] private int maxPlayerHealth = 20;
    private int currentPlayerHealth;

    // UI 참조
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject victoryUI;
    [SerializeField] private GameObject blurOverlay; // 블러 효과용 오버레이

    // 이벤트
    public delegate void OnHealthChanged(int currentHealth, int maxHealth);
    public static event OnHealthChanged onHealthChanged;

    private void Awake()
    {
        // 싱글톤 설정 - DontDestroyOnLoad 제거하여 씬 재로드시 새로 생성
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 게임 시작시 체력 초기화
        currentPlayerHealth = maxPlayerHealth;
        SetGameState(GameState.Playing);
    }

    private void Update()
    {
        // ESC 키로 일시정지
        if (Input.GetKeyDown(KeyCode.Escape) && currentState == GameState.Playing)
        {
            PauseGame();
        }
    }

    // 게임 상태 설정
    public void SetGameState(GameState newState)
    {
        currentState = newState;

        // 모든 UI 비활성화
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        if (gameOverUI != null) gameOverUI.SetActive(false);
        if (victoryUI != null) victoryUI.SetActive(false);
        if (blurOverlay != null) blurOverlay.SetActive(false);

        switch (newState)
        {
            case GameState.Playing:
                Time.timeScale = 1f; // 게임 재개
                break;

            case GameState.Paused:
                Time.timeScale = 0f; // 게임 일시정지
                if (pauseMenuUI != null) pauseMenuUI.SetActive(true);
                break;

            case GameState.GameOver:
                Time.timeScale = 0f; // 게임 정지
                if (gameOverUI != null) gameOverUI.SetActive(true);
                if (blurOverlay != null) blurOverlay.SetActive(true);
                break;

            case GameState.Victory:
                Time.timeScale = 0f; // 게임 정지
                if (victoryUI != null) victoryUI.SetActive(true);
                if (blurOverlay != null) blurOverlay.SetActive(true);
                break;
        }
    }

    // 플레이어 데미지 처리
    public void TakeDamage(int damage)
    {
        currentPlayerHealth -= damage;
        currentPlayerHealth = Mathf.Clamp(currentPlayerHealth, 0, maxPlayerHealth);
        
        // 체력 변경 이벤트 발생
        onHealthChanged?.Invoke(currentPlayerHealth, maxPlayerHealth);

        // 체력이 0이면 게임 오버
        if (currentPlayerHealth <= 0)
        {
            SetGameState(GameState.GameOver);
        }
    }

    // 게임 일시정지
    public void PauseGame()
    {
        SetGameState(GameState.Paused);
    }

    // 게임 재개
    public void ResumeGame()
    {
        SetGameState(GameState.Playing);
    }

    // 게임 재시작
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 메인 메뉴로
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // 게임 종료
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // 게임 클리어 처리
    public void Victory()
    {
        SetGameState(GameState.Victory);
    }

    // 현재 체력 반환
    public int GetCurrentHealth()
    {
        return currentPlayerHealth;
    }

    // 최대 체력 반환
    public int GetMaxHealth()
    {
        return maxPlayerHealth;
    }
    
    // 현재 게임 상태 반환
    public GameState GetCurrentState()
    {
        return currentState;
    }
}
