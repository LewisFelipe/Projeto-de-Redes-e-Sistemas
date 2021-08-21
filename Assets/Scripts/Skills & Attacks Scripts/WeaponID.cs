using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponID : MonoBehaviour
{
    public static WeaponID Instance;
    private BoxCollider weaponCollider;
    public string weaponName;
    public float weaponDamage;
    public string ID;
    private Animator anim;
    public static bool swordEquipped, hammerEquipped, spearEquipped, axeEquipped, bowEquipped;

    void Awake()
    {
        Instance = this;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        weaponCollider = GetComponent<BoxCollider>();
        //ID = weaponName;
        weaponCollider.enabled = false;
    }

    void Update()
    {
        ID = weaponName;
        if(gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }

        if(ID == "BasicHammer" && gameObject.activeSelf)
        {
            anim.SetBool("isHammer", true);
            hammerEquipped = true;
        }
        else
        {
           anim.SetBool("isHammer", false);
           hammerEquipped = false;
        }

        if(ID == "BasicSword" && gameObject.activeSelf)
        {
            anim.SetBool("isSword", true);
            swordEquipped = true;

        }
        else
        {
           anim.SetBool("isSword", false); 
           swordEquipped = false;
        }

        if(ID == "BasicSpear" && gameObject.activeSelf)
        {
            anim.SetBool("isSpear", true);
            spearEquipped = true;
        }
        else
        {
            anim.SetBool("isSpear", false);
            spearEquipped = false;            
        }

        if(ID == "BasicAxe" && gameObject.activeSelf)
        {
            anim.SetBool("isAxe", true);
            axeEquipped = true;
        }
        else
        {
            anim.SetBool("isAxe", false);
            axeEquipped = false;         
        }

        if(ID == "BasicBow" && gameObject.activeSelf)
        {
            anim.SetBool("isBow", true);
            bowEquipped = true;
        }
        else
        {
            anim.SetBool("isBow", false);
            bowEquipped = false;          
        } 

        StartCoroutine(WeaponColliderFix());

    }

    private IEnumerator WeaponColliderFix()
    {
        if(Input.GetMouseButton(0) && weaponName == "BasicHammer")
        {
            yield return new WaitForSeconds(.5f);
            weaponCollider.enabled = true;
            yield return new WaitForSeconds(.5f);
            weaponCollider.enabled = false;         
        }
        else if(Input.GetMouseButton(0) && weaponName == "BasicSword")
        {
            yield return new WaitForSeconds(.2f);
            weaponCollider.enabled = true;
            yield return new WaitForSeconds(.35f);
            weaponCollider.enabled = false;            
        }
        else if(Input.GetMouseButton(0) && weaponName == "BasicSpear")
        {
            yield return new WaitForSeconds(.15f);
            weaponCollider.enabled = true;
            yield return new WaitForSeconds(.3f);
            weaponCollider.enabled = false;          
        }
        else if(Input.GetMouseButton(0) && weaponName == "BasicAxe")
        {
            yield return new WaitForSeconds(1f);
            weaponCollider.enabled = true;
            yield return new WaitForSeconds(.35f);
            weaponCollider.enabled = false;        
        }
    }
}
