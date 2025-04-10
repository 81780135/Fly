using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    
    [Header("对象池设置")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int initialSize = 20;
    
    private Queue<GameObject> availableBullets = new Queue<GameObject>();
    private Transform poolParent;

    void Awake()
    {
        Instance = this;
        poolParent = new GameObject("BulletPool").transform;
        InitializePool(initialSize);
    }

    void InitializePool(int size)
    {
        for(int i=0; i<size; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, poolParent);
            bullet.SetActive(false);
            availableBullets.Enqueue(bullet);
        }
    }

    public GameObject GetBullet(bool isPlayerBullet, Vector2 position, Vector2 direction)
    {
        if(availableBullets.Count == 0) ExpandPool(5);

        GameObject bullet = availableBullets.Dequeue();
        bullet.transform.position = position;
        bullet.SetActive(true);
        bullet.GetComponent<Bullet>().Initialize(isPlayerBullet, direction);
        return bullet;
    }

    void ExpandPool(int amount)
    {
        for(int i=0; i<amount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, poolParent);
            bullet.SetActive(false);
            availableBullets.Enqueue(bullet);
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        availableBullets.Enqueue(bullet);
    }
}