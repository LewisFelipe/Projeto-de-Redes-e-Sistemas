using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealth : MonoBehaviour
{
    private WeaponID weaponID;
    private GameObject playerPos;
    private Animator animator;
    private int randomAtk;
    private bool isDead;
    private bool isTriggered;
    private bool rangeCooldown;

    //Boss Health bar & UI
    public Image healthBar;
    public float bossHealth;
    private float maxHealth = 2000f;

    private void Start()
    {
        rangeCooldown = true;
        playerPos = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        bossHealth = maxHealth;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Weapon" && !isTriggered)
        {
            weaponID = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponID>();
            DeductHealth(weaponID.weaponDamage);
            isTriggered = true;
            ChangeHealthBar();
            StartCoroutine(TriggerCooldown());
        }

    }

    public void DeductHealth(float deductHealth)
    {

        bossHealth -= deductHealth;
        if(bossHealth <= 0)
        {
            EnemyDead();
            isDead = true;
        }
        else
        {
            isDead = false;
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
            yield return new WaitForSeconds(6f);
            rangeCooldown = true;            
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(4f);
        randomAtk = Random.Range(0, 6);
        animator.SetInteger("AtkID", randomAtk);
    }

    private IEnumerator TriggerCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        isTriggered = false;
    }

    public void EnemyDeathAnim()
    {
        isDead = true;
        animator.SetBool("isDead", true);
    }    

    private void EnemyDead()
    {
        EnemyDeathAnim();
        //Vector3 pos = new Vector3(dropArea.transform.position.x - 1, dropArea.transform.position.y + 1f, dropArea.transform.position.z);
        //GameObject drop = Instantiate(lunarStoneDrop, pos, lunarStoneDrop.transform.rotation);
        //drop.LeanMoveY(1f, .5f);
        //drop.SetActive(false);
        //StartCoroutine(showDrop(drop));
        Destroy(gameObject, 1.5f);
    }

    public void ChangeHealthBar()
    {
        healthBar.fillAmount = bossHealth / maxHealth;
        if(bossHealth >= maxHealth)
        {
            bossHealth = maxHealth;
        }
    }

}
