using System;
using System.Collections;
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
          PlayerStats.TravarPlayer = true;
          PlayerStats.SpawnPosition = coordenadas;
          StartCoroutine(Fade());
      }
   }

   IEnumerator Fade()
   {
       gameObject.GetComponent<FadeScript>().enabled = true;
       yield return new WaitForSeconds(gameObject.GetComponent<FadeScript>().fadeDuration);
       SceneManager.LoadScene(nextScene);
   }
}
