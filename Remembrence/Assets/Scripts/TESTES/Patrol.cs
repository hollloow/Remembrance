using UnityEngine;

public class Patrol : MonoBehaviour
{
    private float wallkingTime = 0;
    [SerializeField] private float speed;
    [SerializeField] private float patrolTime;
    [SerializeField] private float tempoEspera;
    [SerializeField] private int canMoveX;
    [SerializeField] private int canMoveY;
    
    private void FixedUpdate()
    {
        //se andar por esse tempo, mude a dire��o do movimento
        if (wallkingTime >= patrolTime)
        {
            if (tempoEspera + patrolTime <= wallkingTime)
            {
                //anaima��o de virar
                speed *= -1;
                wallkingTime = 0;  
            }
        }
        else
        { transform.Translate(new(speed * Time.deltaTime * canMoveX, speed * Time.deltaTime * canMoveY, 0));}
        
        wallkingTime += Time.deltaTime;
    }
}
