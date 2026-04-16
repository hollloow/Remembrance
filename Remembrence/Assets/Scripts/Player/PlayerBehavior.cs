using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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

    //para o atack
    PlayerAtack Attack;
    private Vector2 lastInput;

    //script do InputSystem
    private InputControls inputC;


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

        Jumping();

        //se o botão de attack foi apertado chame a função de attake
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
                if (jumpTimer >= 0.25f)
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

    private void OnJumpButonReleased(InputAction.CallbackContext obj)
    {
        rb.gravityScale = gravity;
        canJump = false;
    }

    void PickOfTheJump()
    {
        jumpTimer += Time.deltaTime;
    }
    
    private void OnAttack(InputAction.CallbackContext obj)
    {
        if (!Attack.attacking)
        {
            Attack.Atack(lastInput);
        }
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