using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponID : MonoBehaviour
{
    public static WeaponID Instance;
    public string weaponName;
    [HideInInspector] public string ID;
    [HideInInspector] public bool weaponActive;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if(gameObject.activeSelf)
        {
            ID = weaponName;
            weaponActive = true;
        }
        else
        {
            weaponActive = false;
        }
    }
}
