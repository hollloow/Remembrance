using System;
using System.Collections;
using UnityEngine;

public class RangedEnemy : EnemyBase
{
    [SerializeField] private float range;
    [SerializeField] private float collDown;
    [SerializeField] private float distanceShoot;
    private float direction;
    private bool attacking;
    private SpriteRenderer sr;

    PlayerReactions _playerReactions = new PlayerReactions();

    //detect > range

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

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
        
        direction = Math.Sign(playerPosition.x - transform.position.x) ;
        
        float distanceFromPlayer =
            Mathf.Abs(Vector3.Distance(playerPosition, transform.position));

        //se o player tiver detectavel, se aproxime
        //se o player tiver a alcance ataque
        //se o player tiver muito perto corra

        //PRECISA DE ALTERAÇÕES PARA GAMEFELL
        if (!attacking)
        {
            if (distanceFromPlayer <= range / 1.5)
            {
                WalkAway();
            }
            else if (distanceFromPlayer >= range && distanceFromPlayer <= detectRange)
            {
                GetCloser();
            }
            else if (distanceFromPlayer <= range && collDown <= 0)
            {
                StartCoroutine(Attack());
            }
        }
        
    }

    private void WalkAway()
    {
        rb.linearVelocityX = direction * -1 * enemySpeed *Time.deltaTime;
    }
    private void GetCloser()
    {
        rb.linearVelocityX = direction  * enemySpeed * Time.deltaTime;
    }

    
    IEnumerator Attack()
    {
        //casta um raycast, se acertar o player o player toma dano


        //PRECISA DE GAMEFELL
        attacking = true;
        sr.color = Color.yellow;


        yield return new WaitForSeconds(1);
        RaycastHit2D acertou = Physics2D.Raycast(transform.position, Vector2.right * direction, distanceShoot);

        if (acertou)
        {
            if (acertou.collider.gameObject.CompareTag("Player"))
            {
                    _playerReactions.OnHurt(enemyDamage);
            }
        }
        
        sr.color = Color.white;

        //reseta o collDown
        attacking = false;
        collDown = 2.5f;
    }
}
