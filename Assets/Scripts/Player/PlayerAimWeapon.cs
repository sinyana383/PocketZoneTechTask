using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerAimWeapon : MonoBehaviour
{
    public delegate void ShootEventHandler(Vector3 gunEndPointPosition, Vector3 shootDirection);
    public delegate void AnotherEventHandler(ItemData data, int num);
    public static event ShootEventHandler OnShoot;
    public static event AnotherEventHandler OnShootBullets;

    [SerializeField] private Transform aimTransform;
    [SerializeField] private Transform aimGunEndPointTransform;
    
    private Animator aimAnimator;
    [SerializeField] private Animator camAnimator;
    
    [SerializeField] float fireRate = 2f;
    private WaitForSeconds shootDelay;
    private Vector3 direction = Vector3.right;

    private bool haveBullets = false;

    [SerializeField]public ItemData curBullets;
    // may add curWeapon ref

    private void OnEnable()
    {
        Inventory.OnInventoryChangeToBullets += ChangeBulletFlag;
    }

    private void OnDisable()
    {
        Inventory.OnInventoryChangeToBullets -= ChangeBulletFlag;
    }

    public void ChangeBulletFlag(bool state) => haveBullets = state;
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
        Vector3 charLocalScale = Vector3.one;
        if (angle > 90 || angle < -90) {
            aimLocalScale.y = -1f;
            aimLocalScale.x = -1f;
            charLocalScale.x = -1f;
        } else {
            aimLocalScale.y = +1f;
            aimLocalScale.x = +1f;
            charLocalScale.x = +1f;
        }
        aimTransform.localScale = aimLocalScale;
        transform.localScale = charLocalScale;
        // playerLookAt.SetLookAtPosition(mousePosition);
    }

    public void Shoot()
    {
        aimAnimator.SetTrigger("Shoot");

        OnShoot?.Invoke(aimGunEndPointTransform.position, direction);
        OnShootBullets?.Invoke(curBullets, 1);
    }

    // TODO: one shoot
    public IEnumerator RapidFire()
    {
        // haveBullets
        while (haveBullets)
        {
            Shoot();
            yield return shootDelay;
        }
        yield break;
    }
}
