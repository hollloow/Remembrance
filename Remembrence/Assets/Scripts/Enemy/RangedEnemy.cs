using System;
using UnityEngine;

public class RangedEnemy : EnemyBase
{
    [SerializeField] private float range;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float cooldown;

    private void FixedUpdate()
    {
        CheckPlayerPosition();
    }
    
    private void CheckPlayerPosition()
    {
        Vector3 playerPosition = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x,
            GameObject.FindGameObjectWithTag("Player").transform.position.y, 1);
        
        float direction = Math.Sign(playerPosition.x - transform.position.x) ;
        
        float distanceFromPlayer =
            Mathf.Abs(Vector3.Distance(playerPosition, transform.position));

        if (distanceFromPlayer <= detectRange)
        {
            Walk(direction);
        }

        if (distanceFromPlayer <= range)
        {
            Attack();
        }
    }

    private void Walk(float direction)
    {
        rb.linearVelocityX =direction * -1 * enemySpeed *Time.deltaTime;
    }

    private void Attack()
    {
        
    }
}
