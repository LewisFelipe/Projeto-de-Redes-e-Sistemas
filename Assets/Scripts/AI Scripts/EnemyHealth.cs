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
    public GameObject lunarStoneDrop;

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
        if(other.gameObject.tag == "Weapon" && !isTriggered)
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
        enemyAI.isTakingDamage = false;
        isTriggered = false;
    }

    IEnumerator showDrop(GameObject drop)
    {
        yield return new WaitForSeconds(1.49f);
        drop.SetActive(true);
    }

    private void EnemyDead()
    {
        enemyAI.EnemyDeathAnim();
        Vector3 pos = new Vector3(transform.position.x - 1, transform.position.y + 1f, transform.position.z);
        GameObject drop = Instantiate(lunarStoneDrop, pos, lunarStoneDrop.transform.rotation);
        drop.SetActive(false);
        StartCoroutine(showDrop(drop));
        Destroy(gameObject, 1.5f);
    }
}
