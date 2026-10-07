using System;
using System.Collections;
using Unity.Mathematics.Geometry;
using UnityEngine;
using Math = System.Math;

public class JumperEnemy : EnemyBase
{
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private float howCloseToAttack;
    private bool colldown;

    [SerializeField] private float forceX;
    [SerializeField] private float forceY;
    
    private void FixedUpdate()
    {
        if (!colldown && canWalk)
        {
            CheckPlayerInRange();
        }
    }

    
    //checara a localização do player
    //se a distância do inimigo pro player for menor ou igual ao alcance de detectação do inimigo e n tiver atacando
    //siga o player
    private void CheckPlayerInRange()
    {
        Vector3 playerPosition = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x,
            GameObject.FindGameObjectWithTag("Player").transform.position.y, 1);
        
        float direction = Math.Sign(playerPosition.x - transform.position.x) ;
        
        float distanceFromPlayer =
            Mathf.Abs(Vector3.Distance(playerPosition, transform.position));
    
        if (distanceFromPlayer <= howCloseToAttack && !colldown && !dead)
        {
            //se o inimigo tiver perto attack
           StartCoroutine(Attack(direction));   
           colldown = true;
        }
        
        if (distanceFromPlayer <= detectRange  && !dead && !colldown)
        {
            if (!dead)
            {
                FollowPlayer(direction);
                if (distanceFromPlayer < transform.position.x)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    rb.freezeRotation = true;
                }
            }
            
        }
        else
        {
            animator.SetBool("Walking", false);
        }

       
    }

    private void FollowPlayer(float direction)
    {
       
            rb.linearVelocityX = enemySpeed * Time.deltaTime * direction;   
            animator.SetBool("Walking", true);
        
    }

     IEnumerator Attack(float playerPosition)
    {
        yield return new WaitForSeconds(0.35f);

        animator.SetTrigger("Attack");
        
        colldown = true;
        //coloca o attack na posição certa
        print(colldown);
        if (playerPosition > 0)
        {
            rb.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(-forceX, forceY), ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(3.5f);
        colldown = false;
    }
}
