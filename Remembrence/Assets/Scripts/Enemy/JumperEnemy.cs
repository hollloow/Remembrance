using System.Collections;
using Unity.Mathematics.Geometry;
using UnityEngine;
using Math = System.Math;

public class JumperEnemy : EnemyBase
{
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private float howCloseToAttack;
    private bool attacking = false;
    private float colldown;
    
    
    private void FixedUpdate()
    {
        if (!attacking && canWalk)
        {
            CheckPlayerInRange();
        }
    }

    
    //checara a localização do player
    //se a distância do inimigo pro player for menor ou igual ao alcance de detectação do inimigo e n tiver atacando
    //siga o player
    private void CheckPlayerInRange()
    {
        colldown -= Time.deltaTime;

        Vector3 playerPosition = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x,
            GameObject.FindGameObjectWithTag("Player").transform.position.y, 1);
        
        float direction = Math.Sign(playerPosition.x - transform.position.x) ;
        
        float distanceFromPlayer =
            Mathf.Abs(Vector3.Distance(playerPosition, transform.position));
    
        if (distanceFromPlayer <= howCloseToAttack && colldown <= 0 && !dead)
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
        if (!dead)
        {
            rb.linearVelocityX = enemySpeed * Time.deltaTime * direction;   
        }
    }

     IEnumerator Attack(float playerPosition)
    {
        attacking = true;

        yield return new WaitForSeconds(0.35f);

        //coloca o attack na posição certa
        if (playerPosition > 0)
        {
            rb.AddForce(new Vector2(500, 800), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(-500, 800), ForceMode2D.Impulse);
        }
    }
}
