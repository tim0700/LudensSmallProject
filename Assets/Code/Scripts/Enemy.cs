using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 10;
    public float moveSpeed = 2f;
    public int dropCurrency = 10;

    private int currentHealth;
    private bool isDestroyed = false; // 중복 파괴 방지 플래그
    private SpriteRenderer spriteRenderer;

    // 외부에서 접근 가능한 프로퍼티
    public bool IsDestroyed => isDestroyed;

    private void Start()
    {
        currentHealth = maxHealth;

        // EnemyMovement에 이동 속도 전달
        GetComponent<EnemyMovement>().SetSpeed(moveSpeed);

        // SpriteRenderer 캐싱
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 외부에서 파괴 플래그를 설정할 수 있는 메서드
    public void MarkAsDestroyed()
    {
        isDestroyed = true;
    }

    private void Die()
    {
        if (isDestroyed) return;

        isDestroyed = true;

        // 화폐 드롭
        if (LevelManager.main != null)
        {
            LevelManager.main.IncreaseCurrency(dropCurrency);
        }

        // 적 파괴 이벤트 발생
        EnemySpawner.onEnemyDestroy.Invoke();

        // 점점 사라지기 효과 시작
        StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        float duration = 1.0f; // 사라지는 데 걸리는 시간
        float elapsed = 0f;
        Color originalColor = spriteRenderer.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        Destroy(gameObject);
    }
}

