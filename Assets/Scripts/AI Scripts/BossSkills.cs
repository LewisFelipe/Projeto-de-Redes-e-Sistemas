using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkills : MonoBehaviour
{
    private Animator animator;
    private BossHealth bossStats;
    private bool isHealing = false;
    public SphereCollider rightHand, LeftHand;

    void Start()
    {
        animator = GetComponent<Animator>();
        bossStats = GetComponent<BossHealth>();
    }

    
    void Update()
    {
        CheckAnimations();
    }

    private void CheckAnimations()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("EnemyHeal") && isHealing == false)
        {
            bossStats.bossHealth += 100;
            isHealing = true;
            bossStats.ChangeHealthBar();
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("EnemyAttack01"))
        {
            isHealing = false;
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("EnemyAttack02"))
        {
            rightHand.enabled = true;
            LeftHand.enabled = true;
            isHealing = false;
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsTag("EnemyAttack02") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            rightHand.enabled = false;
            LeftHand.enabled = false;
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsTag("EnemyAttack03"))
        {
            rightHand.enabled = true;
            LeftHand.enabled = true;
            isHealing = false;
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsTag("EnemyAttack03") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            rightHand.enabled = false;
            LeftHand.enabled = false;                
        }
    }


}
