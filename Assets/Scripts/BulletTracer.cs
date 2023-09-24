using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTracer : MonoBehaviour
{
    [SerializeField] private PlayerAimWeapon playerAimWeapon;

    private void OnEnable() {
        playerAimWeapon.OnShoot += Fire;
    }
    
    private void OnDisable() {
        playerAimWeapon.OnShoot -= Fire;
    }
    
    private void Fire(object sender, PlayerAimWeapon.OnShootEventArgs e)
    {
        Debug.Log(e.gunEndPointPosition + " " + e.shootDirection * 10f);
        Debug.DrawLine(e.gunEndPointPosition, 
            e.gunEndPointPosition + e.shootDirection * 10f, Color.white, 0.1f);
    }
}
