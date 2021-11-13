using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicAttack : MonoBehaviour
{
    MovementManager mManager = new MovementManager();
    public static BasicAttack Instance;
    private Animator animator;
    private Rigidbody rb;
    private bool isBlocking = false;
    private BoxCollider weaponCollider;
    private GameObject[] weapon;
    private PlayerInputManager playerStats;
    public float defautSpeed = 5f;
    private PlayerHealth playerHealth;
    private NpcWizard npcWizard;
    private bool canAttack = true;
    public static bool isAttacking = false;

    public int cooldown;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        weapon = GameObject.FindGameObjectsWithTag("Weapon");
        weaponCollider = GameObject.FindGameObjectWithTag("Weapon").GetComponent<BoxCollider>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        npcWizard = FindObjectOfType<NpcWizard>();
        weaponCollider.enabled = false;
        playerStats = FindObjectOfType<PlayerInputManager>();
    }

    void Update()
    {

        if(playerStats.speed <= 0)
        {
            animator.SetBool("isWalking", false);
        }

        if(!NpcDialogue.isShopping)
        {
            BasicSword();
            BasicHammer();
            BasicSpear();
            BasicAxe();
            BasicBow();
            UseHealthPotion();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        StartCoroutine(EnableCollider());
    }

    private void UseHealthPotion()
    {
        if(NpcWizard.potionsCount > 0 && playerHealth.health < playerHealth.maxHealth && playerStats.usePotion)
        {
            playerHealth.health += 25;
            NpcWizard.potionsCount--;
            npcWizard.potionsCountText.text = NpcWizard.potionsCount.ToString();
            playerHealth.ChangeHealthBar();
        }
    }

    private void BasicSword()
    {
        
        if(WeaponID.swordEquipped == true) //Basic Sword
        {
            animator.SetBool("isSword", true);

            if(playerStats.attackButtonPressed && canAttack)
            {
                animator.SetTrigger("Attacking");
                int randomAtk = Random.Range(0, 3);
                animator.SetInteger("AtkID", randomAtk);
                StartCoroutine(AttackCooldown());
            }
            else
            {
                animator.ResetTrigger("Attacking");
            }

            /*if(Input.GetMouseButtonDown(1))
            {
                animator.SetTrigger("Blocking");
                animator.SetBool("FinishAnimation", false);
                rb.constraints = RigidbodyConstraints.FreezePosition;
            }
            else if(Input.GetMouseButtonUp(1))
            {
                animator.ResetTrigger("Blocking");
                animator.SetBool("FinishAnimation", true);
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }*/
        }
    }

    private void BasicHammer()
    {
        if(WeaponID.hammerEquipped == true) //Basic Hammer
        {
            animator.SetBool("isHammer", true);

            if(playerStats.attackButtonPressed && canAttack)
            {
                animator.SetTrigger("Attacking");
                playerStats.speed = 0;
                //StartCoroutine(AnimationCooldown());
                StartCoroutine(AttackCooldown());
            }
            else
            {
                animator.ResetTrigger("Attacking");
            }
        }

        
    }

    private void BasicSpear()
    {
        if(WeaponID.spearEquipped == true)
        {
            animator.SetBool("isSpear", true);

            if(playerStats.attackButtonPressed && canAttack)
            {
                animator.SetTrigger("Attacking");
                StartCoroutine(AttackCooldown());
            }
            else
            {
                animator.ResetTrigger("Attacking");
            }                
        }
    }

    private void BasicAxe()
    {
        if(WeaponID.axeEquipped == true)
        {
            animator.SetBool("isAxe", true);

            if(playerStats.attackButtonPressed && canAttack)
            {
                int randomAtk = Random.Range(0, 3);
                animator.SetTrigger("Attacking");
                animator.SetInteger("AtkID", randomAtk);
                StartCoroutine(AttackCooldown());
            }
            else
            {
                animator.ResetTrigger("Attacking");
            }                
        }        
    }

    private void BasicBow()
    {
        if(WeaponID.bowEquipped == true)
        {
            animator.SetBool("isBow", true);

            if(playerStats.attackButtonPressed && canAttack)
            {
                animator.SetTrigger("Attacking");
                StartCoroutine(AttackCooldown());
            }
            else
            {
                animator.ResetTrigger("Attacking");
            }                
        }      
    }

    private IEnumerator EnableCollider()
    {
        if(playerStats.attackButtonPressed && WeaponID.hammerEquipped == true && NpcDialogue.isShopping == false)
        {
            //isAttacking = true;
            playerStats.speed = 0f;
            yield return new WaitForSeconds(1f);
            weaponCollider.enabled = true;
            yield return new WaitForSeconds(.75f);
            weaponCollider.enabled = false;
           // isAttacking = false;
            playerStats.speed = defautSpeed;
        }
        else if(playerStats.attackButtonPressed && WeaponID.swordEquipped == true && NpcDialogue.isShopping == false)
        {
            //isAttacking = true;
            playerStats.speed = 0f;
            yield return new WaitForSeconds(.5f);
            weaponCollider.enabled = true;
            yield return new WaitForSeconds(.5f);
            weaponCollider.enabled = false;
            //isAttacking = false;
            playerStats.speed = defautSpeed;      
        }
        else if(playerStats.attackButtonPressed && WeaponID.spearEquipped == true && NpcDialogue.isShopping == false)
        {
            //isAttacking = true;
            playerStats.speed = 0f;
            yield return new WaitForSeconds(.2f);
            weaponCollider.enabled = true;
            yield return new WaitForSeconds(.8f);
            weaponCollider.enabled = false;
            //isAttacking = false;
            playerStats.speed = defautSpeed;       
        }
        else if(playerStats.attackButtonPressed && WeaponID.axeEquipped == true && NpcDialogue.isShopping == false)
        {
            //isAttacking = true;
            playerStats.speed = 0f;
            yield return new WaitForSeconds(.2f);
            weaponCollider.enabled = true;
            yield return new WaitForSeconds(1.15f);
            weaponCollider.enabled = false;
            //isAttacking = false;
            playerStats.speed = defautSpeed;
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(1.5f);
     
        canAttack = true;
    }
    /////////////////////////////////////////////////////NewCode///////////////////////////////////////////////////////
}