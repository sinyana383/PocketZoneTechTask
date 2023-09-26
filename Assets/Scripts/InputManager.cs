using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput input;
    private PlayerInput.PlayerActions playerActions;

    private PlayerMovement playerMovement;
    private PlayerAimWeapon playerAimWeapon;

    private Coroutine fireCoroutine;
    
    private void OnEnable()
    {
        playerActions.Enable();
    }
    private void OnDisable()
    {
        playerActions.Disable();
    }
    private void Awake()
    {
        input = new PlayerInput();
        playerActions = input.Player;

        playerMovement = GetComponent<PlayerMovement>();
        playerAimWeapon = GetComponent<PlayerAimWeapon>();
        
        playerActions.Shoot.started += ctx => StartFiring();
        playerActions.Shoot.canceled += ctx => StopFiring();
    }

    // ??? Change to Update
    private void FixedUpdate()
    {
        playerMovement.ProcessMove(playerActions.Move.ReadValue<Vector2>());
        playerAimWeapon.Aim(playerActions.Move.ReadValue<Vector2>());
    }

    void StartFiring()
    {
        fireCoroutine = StartCoroutine(playerAimWeapon.RapidFire());
    }
    void StopFiring()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
        }
    }
}
