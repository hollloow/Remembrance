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

    //para o attack
    PlayerAtack Attack;
    private Vector2 lastInput;

    //script do InputSystem
    private InputControls inputC;


    #region Setando_Variaveis
    private void OnEnable()
    {
        inputC = new InputControls();
        inputC.Enable();
        inputC.Player.Jump.canceled += OnJumpButonReleased;
        inputC.Player.Attack.started += OnAttack;
        inputC.Player.Magic.started += OnSpecial; 
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
    

    #endregion


    //update q faz o player andar e pular
    private void FixedUpdate()
    {
        //verifica se o player ta morto
        if (!PlayerStats.Dead)
        {
            //determinando ond o ataque vai ser direcionado pela ultima tecla q o jogador clicou 
            if (inputC.Player.Move.ReadValue<Vector2>().x != 0 || inputC.Player.Move.ReadValue<Vector2>().y != 0)
            {
                lastInput = inputC.Player.Move.ReadValue<Vector2>();
            }
            else
            {
                lastInput = new Vector2(lastInput.x, 0);
            }

            //movimentação por linearVelocity
            move = inputC.Player.Move.ReadValue<Vector2>().x;
            rb.linearVelocity = new Vector2(move * playerSpeed * Time.deltaTime, rb.linearVelocity.y);
            //se estiver se movendo cmc a animação
            //se n para a animação
            if (move != 0)
            {
                OnRunning(move);
            }
            else
            {
                animator.SetBool(Running, false);
            }
            
            Jumping();
        

            //se o player tomou dano, por um segundo n toma mais nenhum dano.
            if (PlayerStats.invincibility)
            {
                //contador da invencibilidade
                PlayerStats.InInvincibility += Time.deltaTime;
                
                //animação invencibilidade
                if (GetComponent<SpriteRenderer>().enabled == true)
                {
                    GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    GetComponent<SpriteRenderer>().enabled = true;
                }
                
                //quando acabar a invencibilidade: reseta as variaveis
                if (PlayerStats.InInvincibility >= PlayerStats.invincibilityTime)
                {
                    PlayerStats.InInvincibility = 0;
                    PlayerStats.invincibility = false;
                    GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
    }
    
    //cancelar movimentação
    //se eu quiser cancelar a movimentação quando o player tiver atacando
    
    // void CancelMove()
    // {
    //     if (playerSpeed != 0 && canJump)
    //     {
    //         playerSpeed = 0;
    //     }
    //     else
    //     {
    //         playerSpeed = 200;
    //     }
    // }


    #region Na_morte
    public void OnDeath()
    {
        //  ESPAÇO PARA ANIMAÇÃO DE MORTE E MAIS COISAS LEGAIS :D
        
        //cmc a animação de morte
        animator.SetTrigger(Dying);
        
       
    }

    void OnFinishDeathAnimation()
    {
        //quando acabar a animação de morte
        Destroy(gameObject);
        
        //coisas que acontecem após a morte do player (sla)
    }
    #endregion
    
    

    #region Pulando
    
    void Jumping()
    {
        
        //primeiro checa se já apertou o botão de pulo
        if (inputC.Player.Jump.IsInProgress() && canJump)
        {
            //checa se já esta pulando por mais de 0.5 seg
            //se sim, a gravidade volta ao normal e cmc a animação de queda
            if (jumpTimer >= jumpHolding)
            {
                rb.gravityScale = gravity;
                canJump = false;
                jumpTimer = 0;
                OnFall();
            }
            else
            {
                //ao apertar espaço a gravidade é 0
                //adiciona força no player pra cima, por linearVelocity
                //enquanto o player segurar espaço, por até 0.5 segundos
                rb.gravityScale = 0;
                rb.linearVelocity = new Vector2(move * playerSpeed * Time.deltaTime, jumpForce);
                OnJump();
                PickOfTheJump();
                
            }
        }
    }

    //ao soltar o espaço, a gravidade volta ao normal e cmc a animação de queda
    private void OnJumpButonReleased(InputAction.CallbackContext obj)
    {
        rb.gravityScale = gravity;
        canJump = false;
        OnFall();
    }
    
    //contador do pulo
    void PickOfTheJump()
    {
        jumpTimer += Time.deltaTime;
    }
    
    #endregion



    #region Combate

    //ao apertar o botão de ataque
    //se n tiver atacando, chama o script de attack e cmc a animação de attack
    private void OnAttack(InputAction.CallbackContext obj)
    {
        if (!Attack.attacking)
        {
            Attack.Atack(lastInput);
            OnAttackTrigger();
        }
    }
    
    private void OnSpecial(InputAction.CallbackContext obj)
    {
        //se tiver desbloqueado uma magia e apertar o botão de magia e tiver mana suficiente
        if (PlayerStats.Magic.GetComponent<BaseMagic>().manaCost <= PlayerStats.PlayerMana
            && PlayerStats.Magic != null)
        {
            //lance a magia q vc escolheu e gaste a mana q vc tinha
            GameObject magia=Instantiate(PlayerStats.Magic, transform.position,PlayerStats.Magic.transform.rotation );
            magia.GetComponent<BaseMagic>().ApplyMagicEffect(lastInput.x);
            PlayerReactions playerReac = new PlayerReactions();
            playerReac.OnManaCost(magia.GetComponent<BaseMagic>().manaCost);

        }
    }
    

    #endregion
    


    private void OnCollisionStay2D(Collision2D other)
    {
        
        //ao encostar no chão
        //reseta as variáeis do pulo e termina a animação de queda
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && !canJump)
        {
            jumpTimer = 0;
            canJump = true;
            animator.SetBool(Falling,false);
        }
    }
}