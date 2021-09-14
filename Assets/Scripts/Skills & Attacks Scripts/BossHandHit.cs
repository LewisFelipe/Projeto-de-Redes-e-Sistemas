using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHandHit : MonoBehaviour
{
    PlayerHealth playerHealth;
    private bool isTriggered = false;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !isTriggered)
        {
            if(BossSkills.titanAttacking == true)
            {
                playerHealth.health -= 10;
                playerHealth.ChangeHealthBar();
                isTriggered = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && !isTriggered)
        {
            isTriggered = true;
        }
        else
        {
            isTriggered = false;
        }      
    }
}
