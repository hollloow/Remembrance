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
    protected bool canWalk = true;
    
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
        
        //Knockback
        //desativa a opção de andar do inimigo
        canWalk = false;
        //verifica a direção do player
        float direction = GameObject.FindGameObjectWithTag("Player").transform.position.x -  transform.position.x;
        //lança uma força na direção em q foi atingido
        if (direction > 0)
        {
            rb.AddRelativeForce(-transform.right * knockbackForce, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddRelativeForce(transform.right * knockbackForce, ForceMode2D.Impulse);
        }
        
        //ativa uma corrotina
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
        
        //termina o efeito de knockback
        yield return new WaitForSeconds(0.2f);
        canWalk = true;
    }
    private  void HandleDeath()
    {
        //animação de morte
        Destroy(gameObject);
    }

}
