using Unity.VisualScripting;
using UnityEngine;

public class PlayerReactions
{
    //aqui vai ser um script pra todos as funções que envolva os status do player
    
    
    //função de tomar dano
    //primeiro diminui a vida do player, dpois atualiza a UI e por fim verifica se o player ta morto
    public void OnHurt(int dano)
    {
        PlayerStats.PlayerHp -= dano;
        GameObject.FindWithTag("UI").GetComponent<UIManager>().TxtHPMudar();
        if (PlayerStats.PlayerHp <= 0)
        {
            PlayerStats.Dead = true;
            GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>().OnDeath();
        }
        PlayerStats.invincibility = true;
    }

    public void OnManaCost(int cost)
    {
        PlayerStats.PlayerMana -= cost;
        GameObject.FindWithTag("UI").GetComponent<UIManager>().TxtManaMudar();
    }
}
