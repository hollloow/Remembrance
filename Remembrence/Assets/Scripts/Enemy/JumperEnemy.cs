using System;
using System.Collections;
using Unity.Mathematics.Geometry;
using UnityEngine;
using Math = System.Math;

public class JumperEnemy : EnemyBase
{
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private float howCloseToAttack;
    private bool colldown = false;

    [SerializeField] private float forceX;
    [SerializeField] private float forceY;

    private bool noAr = false;
    
    [SerializeField] private AudioClip attackAudio;
    [SerializeField] private AudioClip atingirChao;
    
    private void FixedUpdate()
    {
        if (!colldown && canWalk && !PlayerStats.Dead)
        {
            CheckPlayerInRange();
        }
    }

    
    //checara a localização do player
    //se a distância do inimigo pro player for menor ou igual ao alcance de detectação do inimigo e n tiver atacando
    //siga o player
    private void CheckPlayerInRange()
    {
        if (dead) { GetComponent<CircleCollider2D>().enabled = false; }

        Vector3 playerPosition = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x,
            GameObject.FindGameObjectWithTag("Player").transform.position.y, 1);
        
        float direction = Math.Sign(playerPosition.x - transform.position.x) ;
        
        float distanceFromPlayer =
            Mathf.Abs(Vector3.Distance(playerPosition, transform.position));
    
        if (distanceFromPlayer <= howCloseToAttack && !colldown && !dead)
        {
            //se o inimigo tiver perto attack
            colldown = true;
            noAr = false;
            StartCoroutine(Attack(direction));
           if (direction > 0)
           {
               GetComponent<SpriteRenderer>().flipX = true;
           }
           else
           {
               GetComponent<SpriteRenderer>().flipX = false;
           }
           rb.freezeRotation = true;
        }
        if (distanceFromPlayer <= detectRange  && !dead && !colldown)
        {
                FollowPlayer(direction);
                if (direction > 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                    rb.freezeRotation = true;
                }
            
        }
        else
        {
            animator.SetBool("Walking", false);
            rb.linearVelocityX = 0;
        }
    }

    private void FollowPlayer(float direction)
    {
       
            rb.linearVelocityX = enemySpeed * Time.deltaTime * direction;   
            animator.SetBool("Walking", true);
            StartCoroutine(OnAudio(started));
    }

     IEnumerator Attack(float playerPosition)
     {
         rb.linearVelocityX = 0;
        yield return new WaitForSeconds(0.5f);

        GetComponent<CircleCollider2D>().enabled = true;
        animator.SetTrigger("Attack");
        
        colldown = true;
        //coloca o attack na posição certa
        if (playerPosition > 0)
        {
            rb.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(-forceX, forceY), ForceMode2D.Impulse);
        }
        StartCoroutine(OnAudio(attackAudio));
        noAr = true;
        yield return new WaitForSeconds(2.0f);
        colldown = false;
    }

     public void CancelAttack()
     {
         GetComponent<CircleCollider2D>().enabled = false;

     }

     
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            print(noAr);
            if (noAr)
            {
                StartCoroutine(OnAudio(atingirChao));
                print(noAr);
            }
            noAr = false;
            rb.linearVelocity = Vector2.zero;
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
