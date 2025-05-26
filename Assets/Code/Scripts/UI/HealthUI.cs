using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;

    private void OnEnable()
    {
        // 체력 변경 이벤트 구독
        GameManager.onHealthChanged += UpdateHealthDisplay;
    }

    private void OnDisable()
    {
        // 이벤트 구독 해제
        GameManager.onHealthChanged -= UpdateHealthDisplay;
    }

    private void Start()
    {
        // 초기 체력 표시
        if (GameManager.Instance != null)
        {
            UpdateHealthDisplay(GameManager.Instance.GetCurrentHealth(), GameManager.Instance.GetMaxHealth());
        }
    }

    // 체력 UI 업데이트
    private void UpdateHealthDisplay(int currentHealth, int maxHealth)
    {
        if (healthText != null)
        {
            healthText.text = $"HP: {currentHealth}/{maxHealth}";
        }
    }
}
