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
    
    private void Awake()
    {
        input = new PlayerInput();
        playerActions = input.Player;

        playerMovement = GetComponent<PlayerMovement>();
        playerAimWeapon = GetComponent<PlayerAimWeapon>();
        
        playerActions.Shoot.performed += _ => playerAimWeapon.Shoot();
    }

    private void FixedUpdate()
    {
        playerMovement.ProcessMove(playerActions.Move.ReadValue<Vector2>());
        playerAimWeapon.Aim(playerActions.Move.ReadValue<Vector2>());
    }
    
    private void OnEnable()
    {
        playerActions.Enable();
    }
    private void OnDisable()
    {
        playerActions.Disable();
    }
}
