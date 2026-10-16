using System;
using TMPro;
using UnityEngine;

public class Idioma : MonoBehaviour
{
    private TextMeshProUGUI texto;
    [SerializeField] private string textoPortugues;
    [SerializeField] private string textoIngles;
    private void OnEnable()
    {
        texto =  GetComponent<TextMeshProUGUI>();
        if (PlayerStats.Idioma)
        {texto.text = textoPortugues; }
        else
        {texto.text = textoIngles; }
    }
}
