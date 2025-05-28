using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI roundText;
    
    [Header("Debug Settings")]
    [SerializeField] private bool showDebugInfo = false;
    [SerializeField] private TextMeshProUGUI debugText; // 선택사항: 디버그 정보용 추가 텍스트

    private void OnEnable()
    {
        // Round 정보 변경 이벤트 구독 (새로운 시그니처에 맞춰 수정)
        EnemySpawner.onRoundInfoChanged += UpdateRoundDisplay;
    }

    private void OnDisable()
    {
        // 이벤트 구독 해제
        EnemySpawner.onRoundInfoChanged -= UpdateRoundDisplay;
    }

    private void Start()
    {
        // 초기 라운드 정보 표시
        UpdateRoundDisplay(1, 3, 0, 0, 0); // 기본값으로 초기화
    }

    // 라운드 UI 업데이트
    private void UpdateRoundDisplay(int currentRound, int maxRounds, int totalEnemies, int remainingEnemies, int enemiesInField)
    {
        if (roundText != null)
        {
            // 총 적 수와 남은 수, 필드에 있는 수를 모두 표시
            roundText.text = $"Round: {currentRound}/{maxRounds} | Total: {totalEnemies} | Remaining: {remainingEnemies} | In Field: {enemiesInField}";
        }
        
        // 디버그 정보 표시 (문제 해결을 위해)
        if (showDebugInfo)
        {
            Debug.Log($"[RoundUI] Round: {currentRound}/{maxRounds}, Total: {totalEnemies}, Remaining: {remainingEnemies}, In Field: {enemiesInField}");
            
            if (debugText != null)
            {
                debugText.text = $"DEBUG:\nRound: {currentRound}/{maxRounds}\nTotal: {totalEnemies}\nRemaining: {remainingEnemies}\nIn Field: {enemiesInField}";
            }
            
            // 실제 씬의 적 수 확인
            GameObject[] enemiesWithTag = GameObject.FindGameObjectsWithTag("Enemy");
            Enemy[] enemyComponents = FindObjectsOfType<Enemy>();
            Debug.Log($"[RoundUI Debug] Enemies with 'Enemy' tag: {enemiesWithTag.Length}, Enemy components: {enemyComponents.Length}");
        }
    }
}
