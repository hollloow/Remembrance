using System;
using System.Collections;
using UnityEngine;

public class MeleEnemy : EnemyBase
{
    //precisa melhorar o game feel
    
    [SerializeField] private GameObject attackPrefab;
    private bool attacking = false;
    
    private void Update()
    {
        if (!attacking)
        {
            CheckPlayerInRange();
        }
    }

    
    //checara a localização do player
    //se a distancia do inimigo pro player for menor ou igual ao alcance de detectação do inimigo
    //siga o player
    private void CheckPlayerInRange()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        
        float distanceToPlayer = Math.Abs(Vector3.Distance(playerPosition, transform.position));
        if (distanceToPlayer < 3f)
        {
            attacking = true;
            Attack(GameObject.FindGameObjectWithTag("Player").transform.position.x);
        }
        if (distanceToPlayer <= detectRange )
        {
            FollowPlayer(playerPosition);
        }
        
    }

    void Attack(float playerPosition)
    {
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
        attacking = true;
        attackPrefab.GetComponent<BoxCollider2D>().enabled = true;
        attackPrefab.GetComponent<SpriteRenderer>().enabled = true;
        StartCoroutine(Atacando());
    }

    IEnumerator Atacando()
    {
        yield return new WaitForSeconds(0.8f);
        attackPrefab.GetComponent<BoxCollider2D>().enabled = false;
        attackPrefab.GetComponent<SpriteRenderer>().enabled = false;
        attackPrefab.GetComponent<Hurt>().hit =  false;
        attacking = false;
    }
}


