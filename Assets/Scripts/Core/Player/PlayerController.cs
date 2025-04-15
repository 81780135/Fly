using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    private Vector2 targetPosition;

    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.1f;
    private float nextFireTime;

    [SerializeField] private float padding = 0.5f; // 距离屏幕边缘的留空
    private Vector2 minBounds, maxBounds;
    [Header("生命值")]
    public int MaxHealth = 100;
    public int CurrentHealth { get; private set; }

    void Start()
    {
        InitBounds();
        CurrentHealth = MaxHealth;
    }

    // 受伤逻辑
    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
        EventBus.Publish(new PlayerHealthChangedEvent(CurrentHealth));
        if (CurrentHealth <= 0)
        {
            Debug.Log("玩家死亡！");
        }
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0)) + new Vector3(padding, padding, 0);
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1)) - new Vector3(padding, padding, 0);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            targetPosition = Camera.main.ScreenToWorldPoint(touch.position);
        }

        // 平滑移动
        transform.position = Vector2.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        Vector2 clampedPos = new Vector2(
            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y)
        );
        transform.position = clampedPos;

        // 自动发射子弹
        if (Time.time >= nextFireTime)
        {
            GameObject bullet = BulletPool.Instance.GetBullet();
            bullet.transform.position = firePoint.position;
            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * 10f;
            nextFireTime = Time.time + fireRate;
        }
    }
    
    // 碰撞检测示例（玩家受伤逻辑）
    void OnTriggerEnter2D(Collider2D other)
    {
        // 通过Tag判断是否为敌人子弹
        if (other.CompareTag("EnemyBullet"))
        {
            TakeDamage(10);
        }

        // 通过Layer判断是否为敌人（更高效）
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            TakeDamage(20);
        }
    }
    
    private void OnDestroy()
    {
        if (CurrentHealth <= 0)
        {
            EventBus.Publish(new PlayerDeathEvent("血量耗尽"));
        }
    }

    // [SerializeField] float screenEdgeMargin = 0.95f;
    // private Vector2 touchStartPos;

    // void Update()
    // {
    //     if (Input.touchCount > 0)
    //     {
    //         Touch touch = Input.GetTouch(0);
    //         Vector3 targetPos = Camera.main.ScreenToWorldPoint(touch.position);
    //         targetPos.z = 0;
            
    //         // 动态边界计算
    //         float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
    //         float screenHeight = Camera.main.orthographicSize;
    //         targetPos.x = Mathf.Clamp(targetPos.x, -screenWidth * screenEdgeMargin, screenWidth * screenEdgeMargin);
    //         targetPos.y = Mathf.Clamp(targetPos.y, -screenHeight * screenEdgeMargin, screenHeight * screenEdgeMargin);
            
    //         transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    //     }
    // }
}

// public class PlayerController : MonoBehaviour
// {
//     [Header("移动设置")]
//     [SerializeField] float moveSpeed = 5f;
//     [SerializeField] float smoothFactor = 0.2f; // 平滑插值系数
//     [SerializeField] TouchController touchController;
//     [SerializeField] Transform firePoint;
    
//     [Header("射击设置")]
//     [SerializeField] float fireRate = 0.2f;
//     private float nextFireTime;
//     private Rigidbody2D rb;
//     private Vector2 targetPosition;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         touchController = FindObjectOfType<TouchController>();
//         targetPosition = transform.position;
//         StartCoroutine(AutoFire());
//     }
//     void Update()
//     {
        
//         // 仅在有效触摸时更新目标位置
//         if (touchController.IsInputActive)
//         {
//             // Debug.Log("判断后："+touchWorldPos);
//             // 获取触控目标位置
//             Vector2 touchWorldPos = touchController.GetTouchWorldPosition();
//             if (!float.IsNegativeInfinity(touchWorldPos.x) && !float.IsNegativeInfinity(touchWorldPos.y))
//             {
//                 targetPosition = touchWorldPos;
//                 ClampPositionToScreenBounds();
//             }
//         }
//     }

//     void FixedUpdate()
//     {

//         // Debug.Log("目标要移动，当前坐标"+transform.position+"，要移动到的坐标："+targetPosition);
//         // 平滑移动到目标位置
//         rb.MovePosition(Vector2.Lerp(
//             transform.position,
//             targetPosition,
//             smoothFactor * Time.fixedDeltaTime * 50
//         ));
//     }


//     // 限制飞机在屏幕内
//     void ClampPositionToScreenBounds()
//     {
//         Vector2 viewportPos = Camera.main.WorldToViewportPoint(targetPosition);
//         viewportPos.x = Mathf.Clamp(viewportPos.x, 0.05f, 0.95f);
//         viewportPos.y = Mathf.Clamp(viewportPos.y, 0.05f, 0.95f);
//         targetPosition = Camera.main.ViewportToWorldPoint(viewportPos);
//     }

//     IEnumerator AutoFire()
//     {
//         while (true)
//         {
//             BulletPool.Instance.GetBullet(true, firePoint.position, Vector2.up);
//             yield return new WaitForSeconds(fireRate);
//         }
//     }

//     // void HandleMovement()
//     // {
//     //     // float h = Input.GetAxis("Horizontal");
//     //     // float v = Input.GetAxis("Vertical");
//     //     // rb.velocity = new Vector2(h, v) * moveSpeed;
//     //     Vector2 direction = touchController.GetMoveDirection();
//     //     rb.velocity = direction * moveSpeed;
//     // }

//     void HandleShooting()
//     {
//         // for(i=1,i<=10,i++)
//         // {
//         //     BulletPool.Instance.GetBullet(true, firePoint.position, Vector2.up);
//         // }
//         if(Input.GetButton("Fire1") && Time.time >= nextFireTime)
//         {
//             BulletPool.Instance.GetBullet(true, firePoint.position, Vector2.up);
//             nextFireTime = Time.time + fireRate;
//         }
//     }

//     void LateUpdate()
//     {
//         Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
//         viewPos.x = Mathf.Clamp(viewPos.x, 0.05f, 0.95f);
//         viewPos.y = Mathf.Clamp(viewPos.y, 0.05f, 0.95f);
//         transform.position = Camera.main.ViewportToWorldPoint(viewPos);
//     }
// }