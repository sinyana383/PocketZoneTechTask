using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerAimWeapon : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs {
        public Vector3 gunEndPointPosition;
        public Vector3 shootDirection;
    }
    
    [SerializeField] private Transform aimTransform;
    [SerializeField] private Transform aimGunEndPointTransform;
    
    private Animator aimAnimator;
    [SerializeField] private Animator camAnimator;
    
    [SerializeField] float fireRate = 2f;
    private WaitForSeconds shootDelay;
    private Vector3 direction = Vector3.right;
    private void Awake()
    {
        aimAnimator = aimTransform.GetComponent<Animator>();
        shootDelay = new WaitForSeconds(1 / fireRate);
    }
    
    public void Aim(Vector2 dir)
    {
        if (dir.x == 0 && dir.y == 0)
            return;

        Vector3 aimDirection = dir.normalized;
        aimDirection.z = 0f;

        // TODO: LookAt() character realization needed
        if (aimDirection.x != 0 || aimDirection.y != 0)
            direction = aimDirection;
        else
            direction = Vector3.right;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        
        Vector3 aimLocalScale = Vector3.one;
        if (angle > 90 || angle < -90) {
            aimLocalScale.y = -1f;
        } else {
            aimLocalScale.y = +1f;
        }
        aimTransform.localScale = aimLocalScale;
        // playerLookAt.SetLookAtPosition(mousePosition);
    }

    public void Shoot()
    {
        aimAnimator.SetTrigger("Shoot");
        camAnimator.SetTrigger("Shake");
        
        OnShoot?.Invoke(this, new OnShootEventArgs { 
            gunEndPointPosition = aimGunEndPointTransform.position,
            shootDirection = direction,
        });
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
