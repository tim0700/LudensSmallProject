using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 10;
    public float moveSpeed = 2f;
    public int dropCurrency = 10; // ���� ����� �� ����ϴ� ��ȭ ��

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;

        // �̵� �ӵ��� EnemyMovement ��ũ��Ʈ�� ����
        GetComponent<EnemyMovement>().SetSpeed(moveSpeed);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private bool isDestroyed = false; // 중복 파괴 방지 플래그
    
    // 외부에서 접근 가능한 프로퍼티
    public bool IsDestroyed => isDestroyed;
    
    // 외부에서 파괴 플래그를 설정할 수 있는 메서드
    public void MarkAsDestroyed()
    {
        isDestroyed = true;
    }

    private void Die()
    {
        if (isDestroyed) return; // 이미 파괴된 경우 중복 파괴 방지
        
        isDestroyed = true; // 중복 파괴 방지 플래그 설정
        
        // 화폐 드롭 (파괴되기 전에 처리)
        if (LevelManager.main != null)
        {
            LevelManager.main.IncreaseCurrency(dropCurrency);
        }
        
        // 적 파괴 이벤트 발생
        EnemySpawner.onEnemyDestroy.Invoke();
        
        // 게임 오브젝트 제거
        Destroy(gameObject);
    }
}

