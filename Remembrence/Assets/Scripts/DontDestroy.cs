using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    private static GameObject[] persistentObjects = new GameObject[2];
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

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "OuroPretoComeco")
        {
            Destroy(gameObject);
        }
    }
}

