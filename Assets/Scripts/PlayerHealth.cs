﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    [HideInInspector] public int maxHealth = 100;
    private float maxHealthFloat = 100f;
    public Image healthBar;
    public static bool changeMaxLife;
    public TMP_Text maxLifeText;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        //ChangeHealthBar();
        if(changeMaxLife == true)
        {
            maxHealth += 50;
            maxHealthFloat += 50;
            changeMaxLife = false;
            health = maxHealth;
            ChangeHealthBar();
        }

        maxLifeText.text = maxHealth.ToString();
    }

    public void ChangeHealthBar()
    {
        healthBar.fillAmount = health / maxHealthFloat;
        if(health >= maxHealth)
        {
            health = maxHealth;
        }
    }
}
