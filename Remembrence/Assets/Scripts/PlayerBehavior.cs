using System;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // variaveis para a movimentação
    [SerializeField] private int playerSpeed;
    private float move;
    
    //variaveis para o pulo
    [SerializeField] private int jumpForce;
    bool canJump = true;
    float jumpTimer = 0;
    private Rigidbody2D rb;
    float gravity;
    
    //script do InputSystem
    private InputControls inputC;
    

    //setando o inputSystem
    private void OnEnable()
    {
        
        inputC = new InputControls();
        inputC.Enable();
    }
    private void OnDisable()
    {
        inputC.Disable();
    }

    ////setando variaveis iniciais
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = rb.gravityScale;
    }

    //update q faz o player andar e pular
    private void FixedUpdate()
    {
        //movimentação por linearVelocity
        move = inputC.Player.Move.ReadValue<Vector2>().x;
        rb.linearVelocity = new Vector2(move * playerSpeed * Time.deltaTime, rb.linearVelocity.y);
        
        Jumping();
    }

    void Jumping()
    {

        //primeiro checa se já apertou o botão de pulo
        //ao apertar espaço a gravidade é 0
        //adiciona força no player pra cima por linearVelocity enquanto o player segurar espaço por 0.5 segundos
        //ao soltar espaço ou dpois de 0.5 segundos a gravidade volta e esse código n pod tocar até encostar no chao
        if (canJump)
        {
            if (inputC.Player.Jump.IsInProgress())
            {
                if (jumpTimer >= 0.5f)
                {
                    rb.gravityScale = gravity;
                    canJump = false;
                }
                else
                {
                    rb.gravityScale = 0;
                    rb.linearVelocity = new Vector2(move * playerSpeed * Time.deltaTime, jumpForce);
                    PickOfTheJump();
                }
            
            }
            
        }
    }

    private void Update()
    {
        //IMPORTANTE 
        //precisa melhorar
        // isso esta parando o pulo quando solta o espaço
        //como fazer isso fora do update
        if(inputC.Player.Jump.WasReleasedThisFrame())
        {
            rb.gravityScale = gravity;
            canJump = false;
        }
    }

    void PickOfTheJump()
    {
        jumpTimer += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //ao encostar no chao o player pod pular dnovo
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpTimer = 0;
            canJump = true;
        }
    }
}
