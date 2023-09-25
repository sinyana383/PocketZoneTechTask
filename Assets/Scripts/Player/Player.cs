using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float health, maxHealth = 50f;
    
    private bool isGameOver = false;
    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        gameOverScreen.SetActive(false);
    }

    private void Start()
    {
        Time.timeScale = 1;
        health = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            isGameOver = true;
            Gameover();
        }
    }

    private void Gameover()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }
}
