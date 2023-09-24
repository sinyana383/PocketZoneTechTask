using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ??? Maybe IDamageable
public class Zombie : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject player;

    [SerializeField] private float health, maxHealth = 3f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float viewDistance = 5f;
    [SerializeField] private float damage = 2f;
    [SerializeField] private float damageRate = 1f;

    private Transform target;
    private Vector2 moveDirection;

    public bool isPlayerSpotted = false;
    
    private Coroutine damageCoroutine;
    private void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    private void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
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
            Destroy(gameObject);
        }
        viewDistance += viewDistance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable subject))
        {
            Debug.Log("Player entered trigger zone.");
            // Maybe play animation
            damageCoroutine = StartCoroutine(RapidDamage(subject));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (damageCoroutine != null)
        {
            Debug.Log("Player exit trigger zone.");
            StopCoroutine(damageCoroutine);
        }
    }

    public IEnumerator RapidDamage(IDamageable subject)
    {
        while (true)
        {
            Debug.Log("Dealing damage...");
            subject.TakeDamage(damage);
            yield return new WaitForSeconds(1 / damageRate);
        }
    }
    
}
