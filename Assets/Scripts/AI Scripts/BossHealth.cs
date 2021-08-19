using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    private GameObject playerPos;
    private Animator animator;
    private int randomAtk;
    private bool isDead;
    private bool isTriggered;
    private bool rangeCooldown;

    private void Start()
    {
        rangeCooldown = true;
        playerPos = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerPos.transform.position);
        transform.LookAt(playerPos.transform);

        if(distance < 7)
        {
            if(!isTriggered && !isDead)
            {
                StartCoroutine(Cooldown());
                StartCoroutine(AnimationCooldown());
            }
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private IEnumerator AnimationCooldown()
    {
        while(rangeCooldown == true)
        {
            animator.SetBool("isAttacking", false);
            yield return new WaitForSeconds(8f);
            rangeCooldown = false;
        }

        while(rangeCooldown == false)
        {
            animator.SetBool("isAttacking", true);
            yield return new WaitForSeconds(4f);
            rangeCooldown = true;            
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(4f);
        randomAtk = Random.Range(0, 6);
        animator.SetInteger("AtkID", randomAtk);
    }
}
