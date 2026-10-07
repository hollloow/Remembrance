using System;
using Unity.VisualScripting;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private float wallkingTime = 0;
    [SerializeField] private float speed;
    [SerializeField] private float patrolTime;
    [SerializeField] private float tempoEspera;
    [SerializeField] private int canMoveX;
    [SerializeField] private int canMoveY;
    [SerializeField] private Animator anim;

    private void Start()
    {
        if (speed > 0)
        { GetComponent<SpriteRenderer>().flipX = true; }
        else
        { GetComponent<SpriteRenderer>().flipX = false; }
    }

    private void FixedUpdate()
    {
       
        //se andar por esse tempo, mude a dire��o do movimento
        if (wallkingTime >= patrolTime)
        {
            if (anim)
            {
                anim.SetBool("Walk",false);
            }
            if (tempoEspera + patrolTime <= wallkingTime)
            {
                //anaima��o de virar
                speed *= -1;
                wallkingTime = 0;
                if (speed > 0)
                { GetComponent<SpriteRenderer>().flipX = true; }
                else
                { GetComponent<SpriteRenderer>().flipX = false; }
            }
        }
        else
        {
            transform.Translate(new(speed * Time.deltaTime * canMoveX, speed * Time.deltaTime * canMoveY, 0));
            anim.SetBool("Walk",true);
        }
        
        wallkingTime += Time.deltaTime;
    }
}
