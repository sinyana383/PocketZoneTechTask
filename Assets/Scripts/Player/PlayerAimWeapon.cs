using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerAimWeapon : MonoBehaviour
{
    [SerializeField] private Transform aimTransform;

    [SerializeField] float fireRate = 2f;
    private WaitForSeconds shootDelay;
    private void Awake()
    {
        shootDelay = new WaitForSeconds(1 / fireRate);
    }
    
    
    public void Aim(Vector2 direction)
    {
        Vector3 aimDirection = direction.normalized;
        aimDirection.z = 0f;
        
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        
        // Vector3 aimLocalScale = Vector3.one;
        // if (angle > 90 || angle < -90) {
        //     aimLocalScale.y = -1f;
        // } else {
        //     aimLocalScale.y = +1f;
        // }
        // aimTransform.localScale = aimLocalScale;
        // playerLookAt.SetLookAtPosition(mousePosition);
    }

    public void Shoot()
    {
        Debug.Log("Shoot!"); 
    }

    // TODO: one shoot
    public IEnumerator RapidFire()
    {
        while (true)
        {
            Shoot();
            yield return shootDelay;
        }
    }
}
