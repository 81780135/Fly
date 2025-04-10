using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("基础属性")]
    [SerializeField] int maxHealth = 1;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float fireRate = 1f;

    [Header("追逐设置")]
    [SerializeField] float chaseDuration = 2f; // 追逐持续时间
    [SerializeField] float idleDuration = 3f;  // 停止时间
    private Vector2 currentMoveDirection; // 存储当前移动方向

    [Header("射击设置")]
    [SerializeField] Transform player;
    private int currentHealth;
    private bool isChasing = false;

    void Start()
    {
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        currentMoveDirection = Vector2.down; // 初始方向（例如向下移动）
        StartCoroutine(ShootingRoutine());
        StartCoroutine(ChaseBehaviorRoutine()); // 启动追逐协程
    }

    void Update()
    {
        if (player == null) return;

        // 实时更新追逐方向
        if (isChasing)
        {
            currentMoveDirection = (player.position - transform.position).normalized;
        }

        // 无论是否追逐，都按当前方向移动
        Move(currentMoveDirection);
    }

    void Move(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    IEnumerator ShootingRoutine()
    {
        while(true)
        {
            if(player != null)
            {
                Vector2 shootDir = (player.position - transform.position).normalized;
                BulletPool.Instance.GetBullet(false, transform.position, shootDir);
            }
            yield return new WaitForSeconds(fireRate);
        }
    }

    IEnumerator ChaseBehaviorRoutine()
    {
        while (true)
        {
            isChasing = true;
            yield return new WaitForSeconds(chaseDuration); // 追逐时间
            isChasing = false;
            yield return new WaitForSeconds(idleDuration); // 停止时间
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 调用玩家的受伤或死亡方法
            other.GetComponent<PlayerHealth>().TakeDamage(1); // 直接造成致命伤害
            // 或触发游戏结束
            GameManager.Instance.GameOver();
        }
    }
}