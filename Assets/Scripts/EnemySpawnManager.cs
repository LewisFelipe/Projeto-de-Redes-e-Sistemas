using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] enemyType;
    public int[] enemyTypePercent;
    int randEnemyType;
    public float spawnTime;
    public int spawnQuantitie;
    Transform spawnTransform;
    public Vector2 spawnPositionOffset;

    void SpawnEnemy()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        randEnemyType = Random.Range(0, 100);
        int enemyTypeNumberIndex = 0;
        foreach (int percentage in enemyTypePercent)
        {
            if (randEnemyType >= enemyTypePercent[enemyTypeNumberIndex])
            {
                randEnemyType = enemyTypeNumberIndex;
            }
            enemyTypeNumberIndex++;
        }
        if ((randEnemyType + 1) > enemyType.Length)
        {
            randEnemyType = 0;
        }

        Instantiate(enemyType[randEnemyType], spawnTransform.position + new Vector3(spawnPositionOffset.x, 0, spawnPositionOffset.y), spawnTransform.rotation);
    } 

    IEnumerator EnemySpawner()
    {
        yield return new WaitForSeconds(spawnTime);
        for (int i = 0; i < spawnQuantitie; i++)
        {
            SpawnEnemy();
            yield return null;
        }
        StartCoroutine(EnemySpawner());
    }

    void Start()
    {
        spawnTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(EnemySpawner());
    }
}
