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

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && !isTriggered)
        {
            playerHealth.health -= 15;
            playerHealth.ChangeHealthBar();
            isTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player" && !isTriggered)
        {
            isTriggered = false;
        }        
    }
}
