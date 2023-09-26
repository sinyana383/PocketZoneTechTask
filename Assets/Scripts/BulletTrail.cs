using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 targetPos;
    private float progress;

    [SerializeField] private float speed = 40f;
    
    void Start()
    {
        startPos = transform.position.WithAxis(Axis.Z, -1);
    }
    
    void Update()
    {
        progress += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPos, targetPos, progress);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        targetPos = targetPosition.WithAxis(Axis.Z, -1);
    }
}
