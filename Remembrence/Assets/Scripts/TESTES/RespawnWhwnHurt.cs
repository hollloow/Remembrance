using System;
using System.Collections;
using UnityEngine;

public class RespawnWhwnHurt : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GetComponent<Hurt>())
            {
                other.transform.position = PlayerStats.RespawnPosition;
            }
            else
            {
                PlayerStats.RespawnPosition = transform.position;
            }
        }
    }
}
