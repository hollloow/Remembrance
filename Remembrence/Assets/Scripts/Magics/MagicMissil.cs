using System;
using UnityEngine;

public class MagicMissil : BaseMagic
{
    private int damage = 20;
    private float travelSpeed = 12;
    private float lifeTime = 1;
    private float living;

    public override void ApplyMagicEffect(float direction)
    {
        //setando variaveis de acordo com a direção do player
        travelSpeed *= direction;
        PlayerStats.MagicCoolDown = true;
        
        if (direction > 0)
        {
            GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipY = false;
        }
    }

    //movimentação e timer de vida do missil
    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * travelSpeed * Time.fixedDeltaTime);
        lifeTime -= Time.fixedDeltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    //colider do missil e dar dano ao contato com um inimigo
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damageble"))
        {
            other.gameObject.GetComponent<EnemyBase>().Damaged(damage);
            GameManager freze = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            freze.Freze(0.5f);
            Destroy(gameObject);
        }
        else if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Attack"))
        {
            Destroy(gameObject);
        }
    }

    //resetar o coolDown de magias
    private void OnDestroy()
    {
        PlayerStats.MagicCoolDown = false;
    }
}
