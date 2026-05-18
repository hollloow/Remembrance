using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //aqui vai ser o script q tem todos as alterações da UI
    
    //UI do HP
    [SerializeField] private TextMeshProUGUI txtHP;
    [SerializeField] private Image HPFiller;
    
    //UI do MP
    [SerializeField] private TextMeshProUGUI txtMana;
    [SerializeField] private Image MPFiller;

    private void Awake()
    {
        //setando os textos antes (caso tenha alguma alteração)
        TxtHPMudar();
        TxtManaMudar();
    }

    
    //funções de alterar o texto e a barra (vão ser chamadas por fora)
    public void TxtHPMudar()
    {
        txtHP.text = "HP: " + PlayerStats.PlayerHp;

        //mudar a barra de HP
        HPFiller.fillAmount = PlayerStats.PlayerHp * 0.0334f;
    }

    public void TxtManaMudar()
    {
        txtMana.text = "Mana: " + PlayerStats.PlayerMana;

        //Mudar a barra de MP
        MPFiller.fillAmount = PlayerStats.PlayerMana * 0.2f;
    }
    
}
