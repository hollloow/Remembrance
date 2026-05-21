using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //aqui vai ser o script q tem todas as alterações da UI
    
    //UI do HP
    [SerializeField] private TextMeshProUGUI txtHP;
    [SerializeField] private Image hpFiller;
    
    //UI do MP
    [SerializeField] private TextMeshProUGUI txtMana;
    [SerializeField] private Image mpFiller;

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
        hpFiller.fillAmount =(float)PlayerStats.PlayerHp / PlayerStats.PlayerMaxHp;
    }

    public void TxtManaMudar()
    {
        //atualizar o texto
        txtMana.text = "Mana: " + PlayerStats.PlayerMana;
        //atualizar a barra de MP
        mpFiller.fillAmount = (float)PlayerStats.PlayerMana / PlayerStats.PlayerManaMax;
    }
    
}
