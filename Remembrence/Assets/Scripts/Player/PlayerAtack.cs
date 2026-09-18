using System;
using System.Collections;
using UnityEngine;

public class PlayerAtack : MonoBehaviour
{
    [SerializeField] GameObject hitBox;
    [SerializeField] AudioClip attackAudio;
    Transform trans;

    private float colldowntimer;
    private bool cooldowncounting = false;
    
    private float speed;
    
    public bool coolDown = false;

    private void Start()
    {
        trans = hitBox.GetComponent<Transform>();
    }
    
    public void Atack(Vector2 lastInput)
    {
        coolDown = true;

        speed = GetComponent<PlayerBehavior>().playerSpeed;
        GetComponent<PlayerBehavior>().playerSpeed *= 0.8f;
        //ao apertar o bot�o de ataque
        //defina a posi��o do ataque dependendo do ultimo bot�o q o player apertou
        
            if (lastInput.x > 0)
            {
                trans.transform.rotation = Quaternion.Euler(0, 0, 0);
                trans.transform.localPosition = new(0,-0.08f,0);
                trans.transform.localScale = new Vector3(1, 1, 1);
            }
            else if(lastInput.x < 0)
            {
                trans.transform.rotation = Quaternion.Euler(-1.12f, -0.08f, 0);
                trans.transform.localPosition = new(0, 0, 0);
                trans.transform.localScale = new Vector3(-1, 1, 1);
            }
        //ative o collider e o sprite
        hitBox.GetComponent<BoxCollider2D>().enabled = true;
        
        //Tocar audio
       // GameObject.FindWithTag("GameController").GetComponent<GameManager>().AudioManager(attackAudio,transform,10f);
    }

    void OnAttackCancel()
    {
        //quando a animação de attack terminar
        hitBox.GetComponent<BoxCollider2D>().enabled = false;
        hitBox.GetComponent<Attack>().hit = false;
        cooldowncounting = true;
        GetComponent<PlayerBehavior>().playerSpeed = speed;
    }

    private void FixedUpdate()
    {
        if (cooldowncounting)
        {
            colldowntimer += Time.fixedDeltaTime;
            if (colldowntimer >= 0.2f)
            {
                cooldowncounting = false;
                colldowntimer = 0;
                coolDown = false;
            }
        }
    }
}
