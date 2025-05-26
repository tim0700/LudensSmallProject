using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 10;
    public float moveSpeed = 2f;
    public int dropCurrency = 10; // 적이 사망할 때 드랍하는 재화 양

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

    private bool isDestroyed = false; // 중복 파괴 방지 플래그

    private void Die()
    {

        if (isDestroyed) return; // 이미 파괴된 경우 중복 파괴 방지, 버그 생길수도 있음.


        EnemySpawner.onEnemyDestroy.Invoke(); // 웨이브 카운트 감소
        isDestroyed = true; // 중복 파괴 방지 플래그 설정
        Destroy(gameObject); // 게임에서 제거

        // 사망시 재화 추가
        LevelManager.main.IncreaseCurrency(dropCurrency); // 예시로 dropCurrency 재화 추가, 적 종류마다 다르게 해줘야함


    }
}

