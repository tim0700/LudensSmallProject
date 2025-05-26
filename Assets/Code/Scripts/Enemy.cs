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

    private bool isDestroyed = false; // �ߺ� �ı� ���� �÷���

    private void Die()
    {

        if (isDestroyed) return; // �̹� �ı��� ��� �ߺ� �ı� ����, ���� ������� ����.


        EnemySpawner.onEnemyDestroy.Invoke(); // ���̺� ī��Ʈ ����
        isDestroyed = true; // �ߺ� �ı� ���� �÷��� ����
        Destroy(gameObject); // ���ӿ��� ����

        // ����� ��ȭ �߰�
        LevelManager.main.IncreaseCurrency(dropCurrency); // ���÷� dropCurrency ��ȭ �߰�, �� �������� �ٸ��� �������


    }
}

