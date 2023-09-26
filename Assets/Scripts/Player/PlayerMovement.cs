using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public Vector2 moveVector = Vector2.zero;
    [SerializeField]private float moveSpeed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ProcessMove(Vector2 vecMove)
    {
        // ??? Time.deltaTime leave or not
        rb.velocity = vecMove * (moveSpeed * Time.deltaTime);
    }
}
