using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShockwave : MonoBehaviour
{
    
    private SphereCollider shockwaveArea;
    private bool isTriggered;
    public Animator bossAnimator;
    PlayerHealth playerHealth;

    void Start()
    {
        shockwaveArea = GetComponent<SphereCollider>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        isTriggered = false;
    }

    void Update()
    {
        if(bossAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Shockwave"))
        {
            isTriggered = false;
            StartCoroutine(StopShockwave());
            StartCoroutine(ShockwaveTriggerCooldown());
        }
        else
        {
            shockwaveArea.enabled = false;
            isTriggered = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && !isTriggered)
        {   
            playerHealth.health -= 5;
            playerHealth.ChangeHealthBar();            
            isTriggered = true;
        }
    }

    private IEnumerator ShockwaveTriggerCooldown()
    {
        yield return new WaitForSeconds(1.2f);
        shockwaveArea.enabled = true;
    }

    private IEnumerator StopShockwave()
    {
        yield return new WaitForSeconds(.05f);
        isTriggered = true;
    }

}
