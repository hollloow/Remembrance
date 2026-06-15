using System;
using System.Collections;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //basicamente, esse script será usado em todos os inimigos
    
    [SerializeField] protected int enemyHP;
    [SerializeField] protected int enemyDamage;
    [SerializeField] protected int enemySpeed;
    [SerializeField] protected float detectRange;
    
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float knockbackForce;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //código de tomar dano e morrer
    public void Damaged(int damage)
    {
        enemyHP -= damage;
        float direction = GameObject.FindGameObjectWithTag("Player").transform.position.x -  transform.position.x;
        if (direction > 0)
        {
            rb.AddRelativeForce(-transform.right * knockbackForce, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddRelativeForce(transform.right * knockbackForce, ForceMode2D.Impulse);
        }
        
        StartCoroutine(Attacked());
        if (enemyHP <= 0)
        { HandleDeath();}
    }

    IEnumerator Attacked()
    {
        //só uma resposta visual pra quando tomar dano
        Color corOriginal = gameObject.GetComponent<SpriteRenderer>().color;
        
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = corOriginal;
    }
    private  void HandleDeath()
    {
        //animação de morte
        Destroy(gameObject);
    }

}
