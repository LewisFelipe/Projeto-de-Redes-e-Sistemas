using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponID : MonoBehaviour
{
    public static WeaponID Instance;
    public string weaponName;
    public float weaponDamage;
    public string ID;
    private Animator anim;
    public static bool swordEquipped, hammerEquipped, spearEquipped;

    void Awake()
    {
        Instance = this;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        //ID = weaponName;
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
    }
}
