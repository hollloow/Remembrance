using System;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    private float startPos;
    private float lenght;
    public Camera cam;
    public float paralaxEffect;

    private void Start()
    {
        startPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float dist =cam.transform.position.x * paralaxEffect;
        float movement = cam.transform.position.x - (1-paralaxEffect);

        // if (movement > startPos + lenght)
        // {
        //     startPos += lenght;
        // }
        // else if (movement < startPos - lenght)
        // {
        //     startPos -= lenght;
        // }
    }
}
