using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;

    private void Start()
    {
        target = LevelManager.main.path[pathIndex];
        Vector3 startPos = transform.position;
        startPos.z = 0f;
        transform.position = startPos;
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex >= LevelManager.main.path.Length)
            {
                // 중복 처리 방지
                Enemy enemyComponent = GetComponent<Enemy>();
                if (enemyComponent != null)
                {
                    if (enemyComponent.IsDestroyed)
                    {
                        return; // 이미 파괴 처리 중이므로 리턴
                    }
                    
                    // 파괴 플래그 설정
                    enemyComponent.MarkAsDestroyed();
                }
                
                // 목표 도달시 플레이어에게 데미지
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.TakeDamage(1);
                }
                
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;
        }
    }
}
