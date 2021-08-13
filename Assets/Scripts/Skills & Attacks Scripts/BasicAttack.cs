using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public static BasicAttack Instance;
    private Animator animator;
    private Rigidbody rb;
    private bool isBlocking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        BasicHit();
    }

    private void BasicHit()
    {
        //Basic Sword
        if(WeaponID.Instance.weaponActive == true && WeaponID.Instance.ID == "BasicSword")
        {
            if(Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Attacking");
            }
            else
            {
                animator.ResetTrigger("Attacking");
            }

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
        }
    }
}
