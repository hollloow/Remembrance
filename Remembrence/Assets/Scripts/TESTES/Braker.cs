using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Braker : MonoBehaviour
{
  [SerializeField] private GameObject partes;
  [SerializeField] private AudioClip quebrar;
  private AudioSource audio;
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Attack") && partes != null)
    {
      partes.transform.SetParent(null);
      partes.SetActive(true);
      AddForceToParts();
      audio = GetComponent<AudioSource>();
      audio.pitch = Random.Range(0.8f, 1.2f);
      audio.clip = quebrar;
      audio.Play();
      GetComponent<SpriteRenderer>().enabled = false;
      StartCoroutine(Morrer());
    }
  }

  IEnumerator Morrer()
  {
    yield return new WaitForSeconds(quebrar.length);
    Destroy(gameObject);
  }

  void AddForceToParts()
  {
    foreach (Transform child in partes.transform)
    {
      if (child.TryGetComponent<Rigidbody2D>(out var rb))
      {
        Vector2 randomDirection = Random.insideUnitCircle;
        float randomForce = Random.Range(1, 5);
        float randomTorque = Random.Range(1, 5);
        
        rb.AddForce(randomDirection*randomForce,ForceMode2D.Impulse);
        rb.AddTorque(randomTorque, ForceMode2D.Impulse);
      }
    }
  }
}
