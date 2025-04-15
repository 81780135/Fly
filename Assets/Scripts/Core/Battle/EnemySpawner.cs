using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemySpawner.cs
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 2f;  
    private bool isSpawning = false;
    void Start()
    {
        List<EnemyConfig> enemyConfigs = ConfigLoader.LoadEnemyConfig();
        // BulletConfig bulletConfig = ConfigLoader.LoadBulletConfig();
        
        // 动态生成敌人
        foreach (EnemyConfig config in enemyConfigs)
        {
            SpawnEnemy(config);
        }
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy(EnemyConfig config)
    {
        if (!isSpawning) return;
        Vector2 spawnPos = new Vector2(Random.Range(-5f, 5f), 7f);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    // [SerializeField] GameObject[] enemyPrefabs;
    // [SerializeField] float spawnInterval = 2f;
    // [SerializeField] Vector2 spawnArea = new Vector2(8, 4);

    // private bool isSpawning = false;

    // // void Start()
    // // {
    // //     Debug.Log("敌人基地创建完成")
    // //     if(Time.timeScale != 0)
    // //         StartCoroutine(SpawnEnemies());
    // // }

    public void StartSpawning()
    {
        isSpawning = true;
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
        // if (!isSpawning)
        // {
        //     StartCoroutine(SpawnEnemies());
        //     isSpawning = true;
        // }
    }
    // 停止生成
    public void StopSpawning()
    {
        isSpawning = false;
        CancelInvoke(nameof(SpawnEnemy));
    }



    // IEnumerator SpawnEnemies()
    // {
    //     while(true)
    //     {
    //         Vector2 spawnPos = new Vector2(
    //             Random.Range(-spawnArea.x, spawnArea.x),
    //             spawnArea.y
    //         );
    //         GameObject enemy = Instantiate(
    //             enemyPrefabs[Random.Range(0, enemyPrefabs.Length)],
    //             spawnPos,
    //             Quaternion.identity
    //         );
    //         yield return new WaitForSeconds(spawnInterval);
    //     }
    // }
}

