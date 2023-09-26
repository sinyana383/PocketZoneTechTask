using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Player player;
    
    [SerializeField] public float viewDistance = 8f;
    [SerializeField] private float moveSpeed = 3f;
    public bool isPlayerSpotted = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Move()
    {
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
    
    void Update()
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

        Move();
    }
}
