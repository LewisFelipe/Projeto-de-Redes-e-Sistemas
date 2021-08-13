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
    bool isTriggered;

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
        if(other.gameObject.tag == "Weapon" && !isTriggered && Input.GetMouseButton(0))
        {
            weaponID = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponID>();
            DeductHealth(weaponID.weaponDamage);
            isTriggered = true;
            if(isDead == false)
            {
                StartCoroutine(TakedDamage());
            }
        }
    }

    IEnumerator TakedDamage()
    {
        enemyAI.TakeDamageAnim();
        yield return new WaitForSeconds(1.5f);
        isTriggered = false;
    }

    private void EnemyDead()
    {
        enemyAI.EnemyDeathAnim();
        Destroy(gameObject, 1.5f);
    }
}
