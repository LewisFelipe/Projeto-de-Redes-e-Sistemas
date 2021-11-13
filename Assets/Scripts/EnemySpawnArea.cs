using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnArea : MonoBehaviour
{
    public GameObject enemy;
    public Transform enemyPos;
    private float repeatRate;
    
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            repeatRate = Random.Range(2, 7);
            InvokeRepeating("EnemySpawner", .5f, repeatRate);
            Destroy(gameObject, 11);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void EnemySpawner()
    {
        Instantiate(enemy, enemyPos.position + Random.insideUnitSphere * 10, enemyPos.rotation);
    }
}
