using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //aqui vai ser o script q tem todos as alterações da UI
    
    //textos da UI
    [SerializeField] private TextMeshProUGUI txtHP;
    [SerializeField] private TextMeshProUGUI txtMana;
    [SerializeField] private TextMeshProUGUI txtDinheiro;

    private void Awake()
    {
        //setando os textos antes (caso tenha alguma alteração)
        txtHP.text = "HP: " + PlayerStats.PlayerHp;
        txtMana.text = "Mana: " + PlayerStats.PlayerMana;
        txtDinheiro.text = "Mangos: " + PlayerStats.Money;
    }

    
    //funções de alterar o texto (vão ser chamadas por fora)
    public void TxtHPMudar()
    {
        txtHP.text = "HP: " + PlayerStats.PlayerHp;
    }

    public void TxtManaMudar()
    {
        txtMana.text = "Mana: " + PlayerStats.PlayerMana;
    }

    public void TxtDinheiroMudar()
    {
        txtDinheiro.text = "Mangos: " + PlayerStats.Money;
    }
    
}
