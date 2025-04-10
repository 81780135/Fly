using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移动设置")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float smoothFactor = 0.2f; // 平滑插值系数
    [SerializeField] TouchController touchController;
    [SerializeField] Transform firePoint;
    
    [Header("射击设置")]
    [SerializeField] float fireRate = 0.2f;
    private float nextFireTime;
    private Rigidbody2D rb;
    private Vector2 targetPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        touchController = FindObjectOfType<TouchController>();
        targetPosition = transform.position;
        StartCoroutine(AutoFire());
    }
    void Update()
    {
        
        // 仅在有效触摸时更新目标位置
        if (touchController.IsInputActive)
        {
            // Debug.Log("判断后："+touchWorldPos);
            // 获取触控目标位置
            Vector2 touchWorldPos = touchController.GetTouchWorldPosition();
            if (!float.IsNegativeInfinity(touchWorldPos.x) && !float.IsNegativeInfinity(touchWorldPos.y))
            {
                targetPosition = touchWorldPos;
                ClampPositionToScreenBounds();
            }
        }
    }

    void FixedUpdate()
    {

        // Debug.Log("目标要移动，当前坐标"+transform.position+"，要移动到的坐标："+targetPosition);
        // 平滑移动到目标位置
        rb.MovePosition(Vector2.Lerp(
            transform.position,
            targetPosition,
            smoothFactor * Time.fixedDeltaTime * 50
        ));
    }


    // 限制飞机在屏幕内
    void ClampPositionToScreenBounds()
    {
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(targetPosition);
        viewportPos.x = Mathf.Clamp(viewportPos.x, 0.05f, 0.95f);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0.05f, 0.95f);
        targetPosition = Camera.main.ViewportToWorldPoint(viewportPos);
    }

    IEnumerator AutoFire()
    {
        while (true)
        {
            BulletPool.Instance.GetBullet(true, firePoint.position, Vector2.up);
            yield return new WaitForSeconds(fireRate);
        }
    }

    // void HandleMovement()
    // {
    //     // float h = Input.GetAxis("Horizontal");
    //     // float v = Input.GetAxis("Vertical");
    //     // rb.velocity = new Vector2(h, v) * moveSpeed;
    //     Vector2 direction = touchController.GetMoveDirection();
    //     rb.velocity = direction * moveSpeed;
    // }

    void HandleShooting()
    {
        // for(i=1,i<=10,i++)
        // {
        //     BulletPool.Instance.GetBullet(true, firePoint.position, Vector2.up);
        // }
        if(Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            BulletPool.Instance.GetBullet(true, firePoint.position, Vector2.up);
            nextFireTime = Time.time + fireRate;
        }
    }

    void LateUpdate()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp(viewPos.x, 0.05f, 0.95f);
        viewPos.y = Mathf.Clamp(viewPos.y, 0.05f, 0.95f);
        transform.position = Camera.main.ViewportToWorldPoint(viewPos);
    }
}