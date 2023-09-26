using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IDamageable
{
    public static event EventHandler OnGameOver;
    public delegate void EventHandler();
    
    [SerializeField] private UIHealthBar uiHealthBar;
    [SerializeField] private float health, maxHealth = 50f;
    
    private void Awake()
    {
        uiHealthBar = GetComponentInChildren<UIHealthBar>();
    }

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        uiHealthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            OnGameOver?.Invoke();
        }
    }
    
}
