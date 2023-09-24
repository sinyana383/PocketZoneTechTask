using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ??? Maybe IDamageable
public class Zombie : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthBar healthBar;

    [SerializeField] private float health, maxHealth = 3f;
    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    private void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
