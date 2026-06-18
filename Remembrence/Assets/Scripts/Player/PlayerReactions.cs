using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerReactions
{
    //aqui vai ser um script pra todas as funções que envolva os status do player
    
    
    //função de tomar dano e curar
    //quando são chamadas elas mudão os status de acordo com a função e o valor dado
    //e dão update na UI
    public void OnHurt(int dano)
    {
        PlayerStats.PlayerHp -= dano;
        GameObject.FindWithTag("UI").GetComponent<UIManager>().TxtHPMudar();
        if (PlayerStats.PlayerHp <= 0)
        {
            PlayerStats.Dead = true;
            GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>().OnDeath();
        }
        //ativar a invencibilidade
        PlayerStats.invincibility = true;

        //caso esteja se curando, cancelar
        GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>().HealingCancelOnDamage();
        
        //fazer a camera mexer
        OnScreenShake(0.1f);
    }

    void OnScreenShake(float impulseAmount)
    {
        GameObject.FindWithTag("Player").GetComponent<CinemachineImpulseSource>().
            GenerateImpulseWithForce(impulseAmount);
    }

    public void OnHeal()
    {
        PlayerStats.PlayerHp += PlayerStats.HealingEffectiveness;
        if (PlayerStats.PlayerHp > PlayerStats.PlayerMaxHp)
        {
            PlayerStats.PlayerHp = PlayerStats.PlayerMaxHp;
        }
        GameObject.FindWithTag("UI").GetComponent<UIManager>().TxtHPMudar();
    }

    
    //funções de aumentar e diminuir a mana
    //quando são chamadas elas mudão os status de acordo com a função e o valor dado
    //e dão update na UI
    public void OnManaCost(float cost)
    {
        PlayerStats.PlayerMana -= cost;
        GameObject.FindWithTag("UI").GetComponent<UIManager>().TxtManaMudar();
    }

    public void OnManaIncrease(int value)
    {
        PlayerStats.PlayerMana += value;
        if (PlayerStats.PlayerMana > PlayerStats.PlayerManaMax)
        {
            PlayerStats.PlayerMana = PlayerStats.PlayerManaMax;
        }
        GameObject.FindWithTag("UI").GetComponent<UIManager>().TxtManaMudar();
    }
    
    //Para salvar a posição q o player tá quando o jogo fechar
    public void OnGameClose(Vector2 newPosition)
    {
        PlayerPrefs.GetFloat("SpawnPosX",newPosition.x);
        PlayerPrefs.GetFloat("SpawnPosY",newPosition.y);
    }
}
