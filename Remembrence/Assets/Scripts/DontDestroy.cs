using System;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static GameObject[] persistentObjects = new GameObject[4];
    [SerializeField] private int objectID;

    private void Awake()
    {
        if (persistentObjects[objectID] == null)
        {
            persistentObjects[objectID] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //n ta funcionando PQQQQQQQ
        if (gameObject.CompareTag("Player") && PlayerStats.posicao != Vector3.zero)
        {
            print(PlayerStats.posicao);
            transform.position = PlayerStats.posicao;
        }
    }
}
