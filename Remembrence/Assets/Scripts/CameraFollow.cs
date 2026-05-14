using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followSpeed;


    private void Update()
    {
        float directionX = Math.Sign(target.position.x - transform.position.x);
        float directionY = Math.Sign(target.position.y - transform.position.y);

        if (target.position.x - transform.position.x != 0 || target.position.y - transform.position.y != 0)
        {
            transform.Translate(directionX * followSpeed * Time.deltaTime, directionY * followSpeed * Time.deltaTime,0);
        }
    }
}
