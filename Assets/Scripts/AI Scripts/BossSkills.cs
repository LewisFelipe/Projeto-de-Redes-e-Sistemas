using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkills : MonoBehaviour
{
    private Animator animator;
    private BossHealth bossStats;
    private bool isHealing = false;

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
    }
}
