using System;
using System.Collections;
using UnityEngine;

public class MeleEnemy : EnemyBase
{
    //precisa melhorar o game feel
    
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private float howCloseToAttack;
    private bool attacking = false;
    private float colldown;
    
    private void FixedUpdate()
    {
        if (!attacking)
        {
            CheckPlayerInRange();
        }
    }

    
    //checara a localização do player
    //se a distancia do inimigo pro player for menor ou igual ao alcance de detectação do inimigo e n tiver atacando
    //siga o player
    private void CheckPlayerInRange()
    {
        colldown -= Time.deltaTime;

        Vector3 playerPosition = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x,
            GameObject.FindGameObjectWithTag("Player").transform.position.y, 1);
        
        float direction = Math.Sign(playerPosition.x - transform.position.x) ;
        
        float distanceFromPlayer =
            Mathf.Abs(Vector3.Distance(playerPosition, transform.position));
    
        if (distanceFromPlayer <= howCloseToAttack && colldown <= 0)
        {
            //se o inimigo tiver perto attack
           StartCoroutine(Attack(direction));   
        }
        
        if (distanceFromPlayer <= detectRange && !attacking)
        {
            FollowPlayer(direction);
        }

       
    }

    private void FollowPlayer(float direction)
    {
        rb.linearVelocityX = enemySpeed * Time.deltaTime * direction;
    }

    IEnumerator Attack(float playerPosition)
    {
        attacking = true;

        yield return new WaitForSeconds(0.35f);

        //coloca o attack na posição certa
        if (playerPosition > 0)
        {
            attackPrefab.transform.localPosition = new Vector3(1, 0, 0);
            attackPrefab.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            attackPrefab.transform.localPosition = new Vector3(-1, 0, 0);
            attackPrefab.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        //seta q ta atacando
        //revela o attack e ativa a hitbox
        attackPrefab.GetComponent<BoxCollider2D>().enabled = true;
        attackPrefab.GetComponent<SpriteRenderer>().enabled = true;

        //cooldown para o attack e para poder atacar e andar dnovo
        yield return new WaitForSeconds(0.8f);
        attackPrefab.GetComponent<BoxCollider2D>().enabled = false;
        attackPrefab.GetComponent<SpriteRenderer>().enabled = false;
        attackPrefab.GetComponent<Hurt>().hit =  false;
        
        yield return new WaitForSeconds(0.15f);
        attacking = false;
        colldown = 1;
    }
}


