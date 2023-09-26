using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// ??? Maybe IDamageable
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private UIHealthBar uiHealthBar;
    [SerializeField] private LevelLogic levelLogic;
    [SerializeField] private Player player;
    [SerializeField] private Transform ground;

    [SerializeField] private float health, maxHealth = 3f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float damageRate = 1f;

    private Transform target;
    private Vector2 moveDirection;
    
    public bool isDamaging = false;
    
    private void Awake()
    {
        uiHealthBar = GetComponentInChildren<UIHealthBar>();
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        levelLogic = FindObjectOfType<LevelLogic>();
        
        health = maxHealth;
        uiHealthBar.UpdateHealthBar(health, maxHealth);
        StartCoroutine(RapidDamage(player.GetComponent<IDamageable>()));
    }
    

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        uiHealthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            GetComponent<LootBag>().InstantiateLoot(ground.position);
            levelLogic.MinusEnemyCount();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable subject) && !isDamaging)
        {
            isDamaging = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out var dam))
        {
            isDamaging = false;
        }
    }

    public IEnumerator RapidDamage(IDamageable subject)
    {
        
        while (true)
        {
            if (isDamaging)
                subject.TakeDamage(damage);
            yield return new WaitForSeconds(1 / damageRate);
        }
    }
}
