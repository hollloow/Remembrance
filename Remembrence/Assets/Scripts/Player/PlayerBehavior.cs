using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : PlayerAnimation
{
    // variaveis para a movimentação
    [SerializeField] private int playerSpeed;
    private float move;

    //variaveis para o pulo
    [SerializeField] private int jumpForce;
    [SerializeField] private float jumpHolding;
    bool canJump = true;
    float jumpTimer = 0;
    private Rigidbody2D rb;
    float gravity;

    //para o atack
    PlayerAtack Attack;
    private Vector2 lastInput;

    //script do InputSystem
    private InputControls inputC;
    
    //testes da camera
    public float directionX;
    public float directionY;

    //setando o inputSystem
    private void OnEnable()
    {
        inputC = new InputControls();
        inputC.Enable();
        inputC.Player.Jump.canceled += OnJumpButonReleased;
        inputC.Player.Attack.started += OnAttack;
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
        Attack = GetComponent<PlayerAtack>();
    }

    //update q faz o player andar e pular
    private void FixedUpdate()
    {
        //movimentação por linearVelocity
        if (inputC.Player.Move.ReadValue<Vector2>().x != 0 || inputC.Player.Move.ReadValue<Vector2>().y != 0)
        {
            lastInput = inputC.Player.Move.ReadValue<Vector2>();
        }

        move = inputC.Player.Move.ReadValue<Vector2>().x;
            rb.linearVelocity = new Vector2(move * playerSpeed * Time.deltaTime, rb.linearVelocity.y);

            if (move != 0)
            {
                directionX = move;
            }
            else
            {
                directionX = 0;
            }
        Jumping();

        //checando se o player ta morto
        if (PlayerStats.Dead)
        {
            //  ESPAÇO PARA ANIMAÇÃO DE MORTE E MAIS COISAS LEGAIS :D
            Destroy(gameObject);
        }

        //se o player tomou dano, por um segundo n toma mais nenhum dano.
        if (PlayerStats.invincibility)
        {
            PlayerStats.InInvincibility += Time.deltaTime;
            if (PlayerStats.InInvincibility >= PlayerStats.invincibilityTime)
            {
                PlayerStats.InInvincibility = 0;
                PlayerStats.invincibility = false;
                //animação invencibilidade
            }
        }
    }

    #region Pulando
    
    void Jumping()
    {
        
        //primeiro checa se já apertou o botão de pulo
        //ao apertar espaço a gravidade é 0
        //adiciona força no player pra cima por linearVelocity enquanto o player segurar espaço por 0.5 segundos
        //ao soltar espaço ou dpois de 0.5 segundos a gravidade volta e esse código n pod tocar até encostar no chao
        if (inputC.Player.Jump.IsInProgress() && canJump)
        {
            if (jumpTimer >= jumpHolding)
            {
                rb.gravityScale = gravity;
                canJump = false;
                directionY = 0;
                jumpTimer = 0;
            }
            else
            {
                directionY = 1;
                rb.gravityScale = 0;
                rb.linearVelocity = new Vector2(move * playerSpeed * Time.deltaTime, jumpForce);
                PickOfTheJump();
            }
        }
    }

    private void OnJumpButonReleased(InputAction.CallbackContext obj)
    {
        rb.gravityScale = gravity;
        canJump = false;
        directionY = 0;
    }

    void PickOfTheJump()
    {
        jumpTimer += Time.deltaTime;
    }
    
    #endregion

    
    
    
    private void OnAttack(InputAction.CallbackContext obj)
    {
        if (!Attack.attacking)
        {
            Attack.Atack(lastInput);
            OnAttackTrigger();
        }
    }


    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && !canJump)
        {
            jumpTimer = 0;
            canJump = true;
        }
    }
}