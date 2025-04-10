using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemySpawner.cs
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] float spawnInterval = 2f;
    [SerializeField] Vector2 spawnArea = new Vector2(8, 4);

    private bool isSpawning = false;

    // void Start()
    // {
    //     Debug.Log("敌人基地创建完成")
    //     if(Time.timeScale != 0)
    //         StartCoroutine(SpawnEnemies());
    // }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            StartCoroutine(SpawnEnemies());
            isSpawning = true;
        }
    }

    IEnumerator SpawnEnemies()
    {
        while(true)
        {
            Vector2 spawnPos = new Vector2(
                Random.Range(-spawnArea.x, spawnArea.x),
                spawnArea.y
            );
            GameObject enemy = Instantiate(
                enemyPrefabs[Random.Range(0, enemyPrefabs.Length)],
                spawnPos,
                Quaternion.identity
            );
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

