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
    PlayerHealth playerHealth;
    public int damage = 10;
    private bool isTriggered;
    [HideInInspector] public bool isTakingDamage;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        Physics.IgnoreCollision(target.GetComponent<Collider>(), GetComponent<Collider>());
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
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
            if(!isTriggered && !isDead)
            {
                //agent.updatePosition = false;
                anim.SetBool("isWalking", false);
                anim.SetBool("isAttacking", true);
                if(anim.GetCurrentAnimatorStateInfo(0).IsTag("EnemyAttack"))
                StartCoroutine(DamageCooldown());          
            }     
        }
    }

    public void TakeDamageAnim()
    {
        isTakingDamage = true;
        anim.SetTrigger("takeDamage");
    }

    public void EnemyDeathAnim()
    {
        isDead = true;
        anim.SetBool("isDead", true);
    }

    IEnumerator DamageCooldown()
    {
        isTriggered = true;
        yield return new WaitForSeconds(.25f);
        if(!isTakingDamage && !isDead)
        playerHealth.health -= damage;
        playerHealth.ChangeHealthBar();
        yield return new WaitForSeconds(1f);
        isTriggered = false;
    }

}
