using UnityEngine;

public class EnemyPatrol : EnemyBase
{
    private float wallkingTime = 0;
    [SerializeField] private float patrolTime;
    [SerializeField] private int canMoveX;
    [SerializeField] private int canMoveY;
    private void FixedUpdate()
    {
        wallkingTime += Time.deltaTime;
        transform.Translate(new(enemySpeed * Time.deltaTime * canMoveX, enemySpeed * Time.deltaTime * canMoveY, 0));

        //se andar por esse tempo, mude a dire��o do movimento
        if (wallkingTime >= patrolTime)
        {
            //anaima��o de virar

            enemySpeed *= -1;
            wallkingTime = 0;
        }
    }
}
