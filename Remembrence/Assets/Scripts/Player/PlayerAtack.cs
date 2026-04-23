using System;
using System.Collections;
using UnityEngine;

public class PlayerAtack : MonoBehaviour
{
    [SerializeField] GameObject hitBox;
    Transform trans;
    public bool attacking;
    [SerializeField]float attackTimer;
    private bool count;

    private void Start()
    {
        trans = hitBox.GetComponent<Transform>();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public Vector2 Atack(Vector2 lastInput)
    {
        //ao apertar o bot�o de ataque
        //defina a posi��o do ataque dependendo do ultimo bot�o q o player apertou
        attacking = true;
        count = true;
        if (lastInput.x > 0)
        {
            trans.transform.rotation = Quaternion.Euler(0, 0, -90);
            trans.transform.localPosition = new(0.8f, 0, 0);
        }
        else if (lastInput.x < 0)
        {
            trans.transform.rotation = Quaternion.Euler(0, 0, 90);
            trans.transform.localPosition = new(-0.8f,0,0);
        }
        else if (lastInput.y > 0)
        {
            trans.transform.rotation = Quaternion.Euler(0, 0, 0);
            trans.transform.localPosition = new(0, 0.8f, 0);
        }
        else if (lastInput.y < 0)
        {
            trans.transform.rotation = Quaternion.Euler(0, 0, 180);
            trans.transform.localPosition = new(0, -0.8f, 0);
        }

        //ative o collider e o sprite
        hitBox.GetComponent<BoxCollider2D>().enabled = true;
        hitBox.GetComponent<SpriteRenderer>().enabled = true;
        return new Vector2();
    }
    private void FixedUpdate()
    {
        //contador para desativar o ataque
        if (count)
        {
            attackTimer += Time.deltaTime;
        }
        if (attackTimer >= 0.2f) 
        {
            count = false;  
            attackTimer = 0;
            CancelAttack();
            StartCoroutine(Colldown());
        }
    }
    //cancelando o ataque
    void CancelAttack()
    {
        hitBox.GetComponent<BoxCollider2D>().enabled = false;
        hitBox.GetComponent<SpriteRenderer>().enabled = false;
        hitBox.GetComponent<Attack>().hit = false;
    }

    IEnumerator Colldown()
    {
        yield return  new WaitForSeconds(0.1f);
        attacking = false;
    }
}
