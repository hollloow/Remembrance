using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class ResPawnPosition : MonoBehaviour
{
    [SerializeField] private Sprite aberta;
    [SerializeField] private Sprite fechada;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == PlayerStats.SceneRespawn)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = aberta;
            GetComponent<Light2D>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = fechada;
        }
    }

    public void LigarLuz()
    {
        GetComponent<Light2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
          GetComponent<Animator>().enabled = true;
        PlayerStats.RespawnPosMorte = other.transform.position;
        PlayerStats.SceneRespawn = SceneManager.GetActiveScene().name;
      }
   }
}
