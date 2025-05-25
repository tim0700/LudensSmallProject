using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 10;
    public float moveSpeed = 2f;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;

        // 이동 속도를 EnemyMovement 스크립트로 전달
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

    private void Die()
    {
        EnemySpawner.onEnemyDestroy.Invoke(); // 웨이브 카운트 감소
        Destroy(gameObject); // 게임에서 제거
    }
}

