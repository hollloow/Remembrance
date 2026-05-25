using UnityEngine;

public class MovingPlataform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            print("sla");
            Debug.Log(collision.gameObject.name);
            collision.gameObject.transform.parent = transform;
        }
    }
}
