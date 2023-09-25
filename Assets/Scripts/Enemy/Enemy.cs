using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// ??? Maybe IDamageable
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform ground;

    [SerializeField] private float health, maxHealth = 3f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float viewDistance = 5f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float damageRate = 1f;

    private Transform target;
    private Vector2 moveDirection;

    public bool isPlayerSpotted = false;
    public bool isDamaging = false;

    private Coroutine damageCoroutine = null;
    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    private void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
        StartCoroutine(RapidDamage(player.GetComponent<IDamageable>()));
    }

    private void Update()
    {
        var distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < viewDistance || isPlayerSpotted)
            isPlayerSpotted = true;
        else
        {
            if (distance > viewDistance + viewDistance)
                isPlayerSpotted = false;
            return;
        }

        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        Vector3 enemyLocalScale = Vector3.one;
        if (angle > 90 || angle < -90) {
            enemyLocalScale.x = -1f;
        } else {
            enemyLocalScale.x = +1f;
        }
        transform.localScale = enemyLocalScale;
        transform.position = Vector2.MoveTowards(transform.position,
            player.transform.position, moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            GetComponent<LootBag>().InstantiateLoot(ground.position);
            Destroy(gameObject);
        }
        viewDistance += viewDistance;
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
