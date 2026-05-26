using System;
using UnityEngine;

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
}
