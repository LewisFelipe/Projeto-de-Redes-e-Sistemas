using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyAI : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    private Animator anim;
    private bool isDead;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if(distance > 2 && isDead == false)
        {
            //agent.updatePosition = true;
            agent.SetDestination(target.position);
            anim.SetBool("isWalking", true);
            anim.SetBool("isAttacking", false);
        }
        else
        {
            //agent.updatePosition = false;
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", true);            
        }
    }

    public void TakeDamageAnim()
    {
        anim.SetTrigger("takeDamage");
    }

    public void EnemyDeathAnim()
    {
        isDead = true;
        anim.SetBool("isDead", true);
    }

}
