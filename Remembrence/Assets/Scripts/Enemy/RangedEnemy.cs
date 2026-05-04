using System;
using UnityEngine;

public class RangedEnemy : EnemyBase
{
    [SerializeField] private float range;
    [SerializeField] private float collDown;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float distanceShoot;
    [SerializeField] private float cooldown;

    PlayerReactions _playerReactions = new PlayerReactions();

    //detect > range

    private void FixedUpdate()
    {
        CheckPlayerPosition();
    }
    
    private void CheckPlayerPosition()
    {
        //passando o tempo do collDown
        collDown -= Time.deltaTime;

        //checando a posição do player, direção do player e a distancia do player para o inimigo
        Vector3 playerPosition = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x,
            GameObject.FindGameObjectWithTag("Player").transform.position.y, 1);
        
        float direction = Math.Sign(playerPosition.x - transform.position.x) ;
        
        float distanceFromPlayer =
            Mathf.Abs(Vector3.Distance(playerPosition, transform.position));

        //se o player tiver detectavel, se aproxime
        //se o player tiver a alcance ataque
        //se o player tiver muito perto corra

        //PRECISA DE ALTERAÇÕES PARA GAMEFELL
        if (distanceFromPlayer <= range/2)
        {
            WalkAway(direction);
        }
        else if (distanceFromPlayer >= range && distanceFromPlayer <= detectRange)
        {
            GetCloser(direction);
        }
        else if (distanceFromPlayer <= range && collDown <= 0)
        {
            Attack(direction);
        }
        
    }

    private void WalkAway(float direction)
    {
        rb.linearVelocityX =direction * -1 * enemySpeed *Time.deltaTime;
    }
    private void GetCloser(float direction)
    {
        rb.linearVelocityX = direction  * enemySpeed * Time.deltaTime;
    }

    private void Attack(float direction)
    {
        //casta um raycast, se acertar o player o player toma dano
        

        //PRECISA DE GAMEFELL
            RaycastHit2D acertou = Physics2D.Raycast(transform.position, Vector2.right * direction, distanceShoot);

            if (acertou.collider.gameObject.CompareTag("Player"))
            {
                _playerReactions.OnHurt(enemyDamage);
            }


        //reseta o collDown
        collDown = 2.5f;
    }
}
