using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletTrail;
    
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float range = 40f;
    // TODO : take damage from weapon data 
    [SerializeField] private float damage = 2f;
    private void OnEnable()
    {
        PlayerAimWeapon.OnShoot += Fire;
    }
    private void OnDisable()
    {
        PlayerAimWeapon.OnShoot -= Fire;
    }

    private void Fire(Vector3 gunEndPointPosition, Vector3 shootDirection)
    {

        var hit = Physics2D.Raycast(gunEndPointPosition, 
            shootDirection, range, layerMask.value);

        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

        var trail = Instantiate(bulletTrail, 
            gunEndPointPosition, transform.rotation);

        var trailScript = trail.GetComponent<BulletTrail>();
        if (hit.collider != null)
        {
            trailScript.SetTargetPosition(hit.point);
            if (hit.collider.gameObject.TryGetComponent<IDamageable>(out IDamageable damObject))
            {
                damObject.TakeDamage(damage);
            }
        }
        else
        {
            var targetPos = gunEndPointPosition + shootDirection * range;
            trailScript.SetTargetPosition(targetPos);
        }
    }
}
