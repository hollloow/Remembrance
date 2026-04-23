using System;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    //basicamente, esse script será usado em todos os inimigos
    
    [SerializeField] protected int enemyHP;
    [SerializeField] protected int enemyDamage;
    [SerializeField] protected int enemySpeed;
    [SerializeField] protected float detectRange;

    
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

    
    //andar até o player (essa função sera chamada pelo codigo do inimigo especifico)
    protected void FollowPlayer(Vector3 playerPosition)
    {
        //movetoeards n é a melhor opção, ver outras
        transform.position = Vector3.MoveTowards(transform.position,
            playerPosition, enemySpeed*Time.deltaTime);
        
    }

   
}
