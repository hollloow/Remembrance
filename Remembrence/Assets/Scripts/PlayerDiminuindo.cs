using System;
using UnityEngine;

public class PlayerDiminuindo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) 
    {

        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.localScale = new Vector3(1.35f, 1.35f, 1);
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.localScale = new Vector3(2, 2, 1);
        }
    }
}
    