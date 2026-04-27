using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //basicamente, esse script será usado em todos os inimigos
    
    [SerializeField] protected int enemyHP;
    [SerializeField] protected int enemyDamage;
    [SerializeField] protected int enemySpeed;
    [SerializeField] protected float detectRange;
    [SerializeField] protected Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //código de tomar dano e morrer
    public void Damaged(int damage)
    {
        enemyHP -= damage;
        if (enemyHP <= 0)
        {
            HandleDeath();
        }
    }
    private  void HandleDeath()
    {
        //animação de morte
        Destroy(gameObject);
    }

}
