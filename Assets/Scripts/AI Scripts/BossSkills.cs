using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkills : MonoBehaviour
{
    private Animator animator;
    private BossHealth bossStats;
    private bool isHealing = false;
    public SphereCollider rightHand, LeftHand;
    public static bool titanAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        bossStats = GetComponent<BossHealth>();
    }

    
    void Update()
    {
        CheckAnimations();
    }

    public void CheckAnimations()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("EnemyHeal") && isHealing == false)
        {
            bossStats.bossHealth += 100;
            isHealing = true;
            bossStats.ChangeHealthBar();
        }

        StartCoroutine(BossDamageCooldown());
              
    }

    private IEnumerator BossDamageCooldown()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("EnemyAttack01") || (animator.GetCurrentAnimatorStateInfo(0).IsTag("EnemyAttack02")) 
        || (animator.GetCurrentAnimatorStateInfo(0).IsTag("EnemyAttack03")))
        {
            isHealing = false;
            yield return new WaitForSeconds(.5f);
            titanAttacking = true;

        }
        else
        {
            titanAttacking = false;
        }        
    }


}
