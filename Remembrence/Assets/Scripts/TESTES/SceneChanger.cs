using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
   [SerializeField] private string nextScene;
   [SerializeField] private Vector3 coordenadas; 
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
            PlayerStats.posicao = coordenadas;
            SceneManager.LoadScene(nextScene);
            
      }
   }
}
