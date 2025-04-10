using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("基本设置")]
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float enemySpeed = 5f;
    [SerializeField] int damage = 1;
    
    [Header("视觉效果")]
    [SerializeField] Color playerBulletColor = Color.blue;
    [SerializeField] Color enemyBulletColor = Color.red;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool isPlayerBullet;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        Debug.LogError("Rigidbody2D 组件缺失！");
        sr = GetComponent<SpriteRenderer>();

    }


    // void FixedUpdate()
    // {
    //     Debug.Log($"当前速度：{rb.velocity}");
    // }

    public void Initialize(bool isPlayerBullet, Vector2 direction)
    {
        this.isPlayerBullet = isPlayerBullet;
        UpdateVisual();
        SetDirection(direction);
    }

    void UpdateVisual()
    {
        sr.color = isPlayerBullet ? playerBulletColor : enemyBulletColor;
    }

    void SetDirection(Vector2 direction)
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        float speed = isPlayerBullet ? playerSpeed:enemySpeed;
        rb.velocity = direction.normalized * speed;
        // 调试信息
        // Debug.Log($"速度已设置：{rb.velocity}");
        RotateSprite(direction);
    }

    void RotateSprite(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(isPlayerBullet && other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
            BulletPool.Instance.ReturnBullet(gameObject);
        }
        else if(!isPlayerBullet && other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            BulletPool.Instance.ReturnBullet(gameObject);
        }
    }

    void OnDisable()
    {
        // 重置状态
        rb.velocity = Vector2.zero;
    }

    void OnBecameInvisible() => BulletPool.Instance.ReturnBullet(gameObject);
}