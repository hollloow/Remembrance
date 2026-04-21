using System;
using UnityEngine;

public class PickupDinheiro : MonoBehaviour
{
    //place holder do código de pegar dinheiro (talvez n)
    
    [SerializeField] private int dinheiroMais;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats.Money++;
            GameObject.FindWithTag("UI").GetComponent<UIManager>().TxtDinheiroMudar();
            Destroy(gameObject);
        }
    }
}
