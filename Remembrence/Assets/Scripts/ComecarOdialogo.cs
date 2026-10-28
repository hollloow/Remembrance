using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComecarOdialogo : MonoBehaviour
{
    [SerializeField] string[] quantidadeDeDialogos;
    [SerializeField] string[] quantidadeDeDialogosIngles;
    [SerializeField] private bool travar;
    [SerializeField] private bool nukarOobjeto;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (SceneManager.GetActiveScene().name == PlayerStats.SceneRespawn)
        { }
        else
        {
            if (other.CompareTag("Player"))
            {
                GameObject.Find("Dialogue").GetComponent<Dialogo>().lines = quantidadeDeDialogos;
                GameObject.Find("Dialogue").GetComponent<Dialogo>().linesIngles = quantidadeDeDialogosIngles;
                GameObject.Find("Dialogue").GetComponent<Dialogo>().travar = travar;
                GameObject.Find("Dialogue").GetComponent<Dialogo>().enabled = true;
                if(nukarOobjeto){GetComponent<BoxCollider2D>().enabled = false;}   
            }   
        }
    }
}
