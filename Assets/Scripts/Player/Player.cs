using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float health, maxHealth = 50f;

    private bool isGameOver = false;
    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        Debug.Log(health);
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            isGameOver = true;
            Gameover();
        }
    }

    private void Gameover()
    {
        // TODO: Disable controls and enable gameover screen
    }
}
