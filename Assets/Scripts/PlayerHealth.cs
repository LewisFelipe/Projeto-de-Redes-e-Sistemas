using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    private int maxHealth = 100;
    public Image healthBar;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        ChangeHealthBar();
    }

    private void ChangeHealthBar()
    {
        healthBar.fillAmount = health / 100f;
    }
}
