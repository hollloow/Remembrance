using System;
using UnityEngine;

public class PlayerAtack : MonoBehaviour
{
    [SerializeField] GameObject hitBox;
    Transform trans;
    public bool attacking;
    float attackTimer;

    private void Start()
    {
        trans = hitBox.GetComponent<Transform>();
    }

    public Vector2 Atack(Vector2 lastInput)
    {
        //ao apertar o botão de ataque
        //defina a posição do ataque dependendo do ultimo botão q o player apertou
        attacking = true;
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
        if (attacking)
        {
            attackTimer += Time.deltaTime;
        }
        if (attackTimer >= 0.2f) 
        {
            attacking = false;
            attackTimer = 0;
            CancelAttack();
        }
    }
    //cancelando o ataque
    void CancelAttack()
    {
        hitBox.GetComponent<BoxCollider2D>().enabled = false;
        hitBox.GetComponent<SpriteRenderer>().enabled = false;
    }
}
