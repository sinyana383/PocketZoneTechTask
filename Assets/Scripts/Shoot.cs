using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Shoot : MonoBehaviour
{
    [SerializeField] private PlayerAimWeapon playerAimWeapon;
    [SerializeField] private GameObject bulletTrail;
    
    [SerializeField] private float range = 40f;

    private void OnEnable() {
        playerAimWeapon.OnShoot += Fire;
    }
    
    private void OnDisable() {
        playerAimWeapon.OnShoot -= Fire;
    }
    
    private void Fire(object sender, PlayerAimWeapon.OnShootEventArgs e)
    {
        Debug.Log(e.gunEndPointPosition + " " + e.shootDirection * range);
        Debug.DrawLine(e.gunEndPointPosition, 
            e.gunEndPointPosition + e.shootDirection * range, Color.white, 0.1f);

        var hit = Physics2D.Raycast(e.gunEndPointPosition, 
            e.shootDirection, range);

        float angle = Mathf.Atan2(e.shootDirection.y, e.shootDirection.x) * Mathf.Rad2Deg;

        var trail = Instantiate(bulletTrail, 
            e.gunEndPointPosition, transform.rotation);

        var trailScript = trail.GetComponent<BulletTrail>();
        if (hit.collider != null)
        {
            trailScript.SetTargetPosition(hit.point);
            // TODO hit the target
        }
        else
        {
            var targetPos = e.gunEndPointPosition + e.shootDirection * range;
            trailScript.SetTargetPosition(targetPos);
        }
    }
}
