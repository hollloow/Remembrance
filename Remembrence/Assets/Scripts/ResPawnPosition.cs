using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class ResPawnPosition : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
          print("papa");
        PlayerStats.RespawnPosMorte = other.transform.position;
        PlayerStats.SceneRespawn = SceneManager.GetActiveScene().name;
        GetComponent<Light2D>().intensity = 3;
      }
   }
}
