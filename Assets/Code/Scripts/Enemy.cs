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

    private void Die()
    {
        EnemySpawner.onEnemyDestroy.Invoke(); // ���̺� ī��Ʈ ����
        Destroy(gameObject); // ���ӿ��� ����
    }
}

