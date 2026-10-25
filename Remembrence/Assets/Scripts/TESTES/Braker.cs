using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Braker : MonoBehaviour
{
  [SerializeField] private GameObject partes;
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Attack") && partes != null)
    {
      partes.transform.SetParent(null);
      partes.SetActive(true);
      AddForceToParts();
      
      Destroy(gameObject);
    }
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
