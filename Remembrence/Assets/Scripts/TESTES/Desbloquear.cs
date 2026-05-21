using System;
using UnityEngine;

public class Desbloquear : MonoBehaviour
{
    [SerializeField] private GameObject Coisa;
   private void OnTriggerEnter2D(Collider2D other)
   {
       if (other.CompareTag("Player"))
       {
           PlayerStats.Magic =  Coisa;
           Destroy(gameObject);
       }
   }
}
