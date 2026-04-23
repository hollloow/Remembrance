using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //variavel de se já acertou o inimigo
    public bool hit = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Damageble") && !hit)
        {
            //OBS: fazer um knockback pra game feel
            other.GetComponent<EnemyBase>().Damaged(PlayerStats.PlayerBasicAttackDamage);
            hit = true;
        }
    }
}
