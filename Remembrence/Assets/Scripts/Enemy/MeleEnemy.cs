using System;
using System.Collections;
using UnityEngine;

public class MeleEnemy : EnemyBase
{
    //precisa melhorar o game feel
    
    [SerializeField] private GameObject attackPrefab;
    private bool attacking = false;
    
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
        Vector3 playerPosition = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x,
            GameObject.FindGameObjectWithTag("Player").transform.position.y, 1);
        
        float direction = Math.Sign(playerPosition.x - transform.position.x) ;
        
        float distanceFromPlayer =
            Mathf.Abs(Vector3.Distance(playerPosition, transform.position));
    
        if (distanceFromPlayer <= 3)
        {
            //se o inimigo tiver perto attack
            Attack(direction);   
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

    void Attack(float playerPosition)
    {
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
        attacking = true;
        attackPrefab.GetComponent<BoxCollider2D>().enabled = true;
        attackPrefab.GetComponent<SpriteRenderer>().enabled = true;
        StartCoroutine(Atacando());
    }

    IEnumerator Atacando()
    {
        //cooldown para o attack e para poder atacar e andar dnovo
        yield return new WaitForSeconds(0.8f);
        attackPrefab.GetComponent<BoxCollider2D>().enabled = false;
        attackPrefab.GetComponent<SpriteRenderer>().enabled = false;
        attackPrefab.GetComponent<Hurt>().hit =  false;
        
        yield return new WaitForSeconds(0.3f);
        attacking = false;
    }
}


