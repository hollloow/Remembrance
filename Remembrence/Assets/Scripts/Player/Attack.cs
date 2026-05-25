using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //variavel de se já acertou o inimigo
    public bool hit = false;
    public int increase = 3;
    private PlayerReactions pr = new PlayerReactions();
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Damageble") && !hit)
        {
            //OBS: fazer um knockback pra game feel
            other.GetComponent<EnemyBase>().Damaged(PlayerStats.PlayerBasicAttackDamage);
            
            pr.OnManaIncrease(increase);
            hit = true;
        }
    }
}
