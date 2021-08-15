using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicAttack : MonoBehaviour
{
    public static BasicAttack Instance;
    private Animator animator;
    private Rigidbody rb;
    private bool isBlocking = false;
    private BoxCollider weaponCollider;
    private GameObject[] weapon;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        weapon = GameObject.FindGameObjectsWithTag("Weapon");
        weaponCollider = GameObject.FindGameObjectWithTag("Weapon").GetComponent<BoxCollider>();
        weaponCollider.enabled = false;
    }
    void Update()
    {
        if(NpcDialogue.isShopping == false)
        {
            BasicSword();
            BasicHammer();
            BasicSpear();
            //BasicHammer();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        StartCoroutine(EnableCollider());
    }

    private void BasicSword()
    {
        
        if(WeaponID.swordEquipped == true) //Basic Sword */
        {
            animator.SetBool("isSword", true);

            if(Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Attacking");
                int randomAtk = Random.Range(0, 3);
                animator.SetInteger("AtkID", randomAtk);
            }
            else
            {
                animator.ResetTrigger("Attacking");
            }

            /*
            if(Input.GetMouseButtonDown(1))
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
            }
            */
        }
    }

    private void BasicHammer()
    {
        if(WeaponID.hammerEquipped == true) //Basic Hammer
        {
            animator.SetBool("isHammer", true);

            if(Input.GetMouseButton(0))
            {
                animator.SetTrigger("Attacking");
                //StartCoroutine(AnimationCooldown());
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
            if(Input.GetMouseButton(0))
            {
                animator.SetTrigger("Attacking");
            }
            else
            {
                animator.ResetTrigger("Attacking");
            }                
        }
    }

    private IEnumerator EnableCollider()
    {
        if(Input.GetMouseButton(0) && WeaponID.hammerEquipped == true)
        {
            yield return new WaitForSeconds(1f);
            weaponCollider.enabled = true;
            yield return new WaitForSeconds(.2f);
            weaponCollider.enabled = false;
        }
        else if(Input.GetMouseButton(0) && WeaponID.swordEquipped == true)
        {
            yield return new WaitForSeconds(.5f);
            weaponCollider.enabled = true;
            yield return new WaitForSeconds(.2f);
            weaponCollider.enabled = false;            
        }
        else if(Input.GetMouseButton(0) && WeaponID.spearEquipped == true)
        {
            yield return new WaitForSeconds(.25f);
            weaponCollider.enabled = true;
            yield return new WaitForSeconds(.35f);
            weaponCollider.enabled = false;             
        }
    }


    IEnumerator AnimationCooldown()
    {
        rb.constraints = RigidbodyConstraints.FreezePosition;
        yield return new WaitForSeconds(1.7f);
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
