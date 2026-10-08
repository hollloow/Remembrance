using System;
using Unity.Cinemachine;
using UnityEngine;

public class ComecarOdialogo : MonoBehaviour
{
    [SerializeField] string[] quantidadeDeDialogos;
    [SerializeField] private bool travar;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.Find("Dialogue").GetComponent<Dialogo>().lines = quantidadeDeDialogos;
            GameObject.Find("Dialogue").GetComponent<Dialogo>().travar = travar;
            GameObject.Find("Dialogue").GetComponent<Dialogo>().enabled = true;
        }
    }
}
