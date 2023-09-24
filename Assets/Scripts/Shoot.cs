using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletTrail;
    
    [SerializeField] private float range = 40f;

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
        // Debug.Log(e.gunEndPointPosition + " " + e.shootDirection * range);
        // Debug.DrawLine(e.gunEndPointPosition, 
            // e.gunEndPointPosition + e.shootDirection * range, Color.white, 0.1f);
            
        var hit = Physics2D.Raycast(gunEndPointPosition, 
            shootDirection, range);

        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

        var trail = Instantiate(bulletTrail, 
            gunEndPointPosition, transform.rotation);

        var trailScript = trail.GetComponent<BulletTrail>();
        if (hit.collider != null)
        {
            trailScript.SetTargetPosition(hit.point);
            // TODO hit the target
        }
        else
        {
            var targetPos = gunEndPointPosition + shootDirection * range;
            trailScript.SetTargetPosition(targetPos);
        }
    }
}
