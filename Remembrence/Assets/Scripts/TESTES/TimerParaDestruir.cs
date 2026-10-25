using System;
using UnityEngine;

public class TimerParaDestruir : MonoBehaviour
{
    private float tempoDeVida = 3;

    private void Update()
    {
        tempoDeVida -= Time.deltaTime;
        if (tempoDeVida <= 0)
        { Destroy(gameObject);}
    }
}
