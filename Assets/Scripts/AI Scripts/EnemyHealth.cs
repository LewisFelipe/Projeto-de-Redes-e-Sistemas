using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float enemyHealth = 100f;
    EnemyAI enemyAI;
    WeaponID weaponID;
    bool takedDamage = false;
    bool isDead = false;

    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
    }

    public void DeductHealth(float deductHealth)
    {

        enemyHealth -= deductHealth;
        if(enemyHealth <= 0)
        {
            EnemyDead();
            isDead = true;
        }
        else
        {
            isDead = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Weapon")
        {
            weaponID = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponID>();
            //isAttacking = true;
            DeductHealth(weaponID.weaponDamage);
            if(isDead == false)
            {
                TakedDamage();
            }
        }
    }

    private void OnTriggerStay()
    {
        return;
    }

    private void TakedDamage()
    {
        enemyAI.TakeDamageAnim();
    }

    private void EnemyDead()
    {
        enemyAI.EnemyDeathAnim();
        Destroy(gameObject, 1.5f);
    }
}
