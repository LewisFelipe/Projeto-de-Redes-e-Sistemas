using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWeaponEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyWeapons;

    void Start()
    {
        int randomIndex = Random.Range(0, enemyWeapons.Length);
        enemyWeapons[randomIndex].SetActive(true);
    }
}
