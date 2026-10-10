using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;
using Object = System.Object;

public class PlayerBehavior : PlayerAnimation
{
    // variaveis para a movimentação
    public float playerSpeed;
    private float playerSpeed2;
    private float move;

    //variaveis para o pulo
    [SerializeField] private int jumpForce;
    [SerializeField] private float jumpHolding;
    [SerializeField] private bool canJump = true;
    private bool isJumping = false;
    private float coyoteTimer;
    float jumpTimer = 0;
    private Rigidbody2D rb;
    float gravity;
    [SerializeField] private PhysicsMaterial2D air;
    [SerializeField] Transform groundCheck;

    //para o attack
    PlayerAtack Attack;
    private Vector2 lastInput;
    
    //para a cura
    private float healingTime;
    private bool healing;

    //script do InputSystem
    private InputControls inputC;
    
    private SpriteRenderer _spriteRenderer;

    [SerializeField] public AudioClip dano;
    [SerializeField] private AudioClip hit;

    #region Setando_Variaveis
    
    private void OnEnable()
    {
        if (GameObject.Find("Poeira"))
        {
            ParticleSystem meuSistemaDeParticulas = GameObject.Find("Poeira").GetComponent<ParticleSystem>();
            meuSistemaDeParticulas.Simulate(20.0f, true, false);
            meuSistemaDeParticulas.Play();   
        }
        inputC = new InputControls();
        playerSpeed2 = playerSpeed;
        inputC.Enable();
        inputC.Player.Jump.canceled += OnJumpButonReleased;
        inputC.Player.Attack.started += OnAttack;
        inputC.Player.Magic.started += OnSpecial;
        inputC.Player.Heal.started += OnHealStart;
        inputC.Player.Menu.started += OnMenu;

        //Pegar infos do player
    }

    private void OnDisable()
    {
        inputC.Disable();
    }

    ////setando variaveis iniciais
    private void Awake()
    {
        if (PlayerStats.Dead)
        {
            transform.position = PlayerStats.RespawnPosMorte;
            PlayerStats.PlayerHp = PlayerStats.PlayerMaxHp;
            PlayerStats.PlayerMana = PlayerStats.PlayerManaMax;
            PlayerStats.Dead = false;
            GameObject.Find("Canvas").GetComponent<UIManager>().TxtHPMudar();
            GameObject.Find("Canvas").GetComponent<UIManager>().TxtManaMudar();
        }
        if (gameObject.CompareTag("Player") && PlayerStats.SpawnPosition != Vector3.zero)
        {
            transform.position = PlayerStats.SpawnPosition;
        }
        rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Attack = GetComponent<PlayerAtack>();
        gravity = rb.gravityScale;
        PlayerStats.TravarPlayer = false;
    }
    

    #endregion


    private void Update()
    {
        if (playerSpeed == 0)
        {
            playerSpeed = playerSpeed2;
        }

        if (!PlayerStats.Dead && !PlayerStats.TravarPlayer)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            IsOnGround();
            Jumping();
            if (canJump)
            {
                OnHeal();
            }
        
            //verifica se o player ta morto ou curando
            if (!healing)
            {
                //determinando ond o ataque vai ser direcionado pela última tecla q o jogador clicou 
                if (inputC.Player.Move.ReadValue<Vector2>().x != 0 )
                {
                    lastInput = inputC.Player.Move.ReadValue<Vector2>();
                }
                else
                {
                    lastInput = new Vector2(lastInput.x, 0);
                }

                //movimentação por linearVelocity
                move = inputC.Player.Move2.ReadValue<float>();
           
                //se estiver se movendo cmc a animação
                //se n para a animação
                if (move != 0)
                {
                    if (move > 0)
                    {
                        GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else if (move < 0)
                    {
                        GetComponent<SpriteRenderer>().flipX = true;
                    }

                    if (!isJumping && !animator.GetBool("Falling") && !healing)
                    {
                        OnRunning();
                    }
                
                }
                else
                {
                    animator.SetBool(Running, false);
                }
            }
        }
        else
        {
            animator.SetBool("Running", false);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX
                             | RigidbodyConstraints2D.FreezeRotation;
        }
      
    }

    //update q faz o player andar e pular
    private void FixedUpdate()
    {
        if (!healing && !PlayerStats.Dead)
        {
            rb.linearVelocity = new Vector2(move * playerSpeed * Time.deltaTime, rb.linearVelocity.y);   
        }
        
        //se o player tomou dano, por um segundo n toma mais nenhum dano.
        if (PlayerStats.invincibility)
        {
            //contador da invencibilidade
            PlayerStats.InInvincibility += Time.deltaTime;
                
            //animação invencibilidade
            if (_spriteRenderer.enabled)
            {
                _spriteRenderer.enabled = false;
            }
            else
            {
                _spriteRenderer.enabled = true;
            }
                
            //quando acabar a invencibilidade: reseta as variaveis
            if (PlayerStats.InInvincibility >= PlayerStats.invincibilityTime)
            {
                PlayerStats.InInvincibility = 0;
                PlayerStats.invincibility = false;
                _spriteRenderer.enabled = true;
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
        //cmc a animação de morte
        animator.SetBool("Dead",true);
    }

    public void OnFinishDeathAnimation()
    {
        StartCoroutine(TrocarSceneNaMorte());
    }

    IEnumerator TrocarSceneNaMorte()
    {
        GetComponent<FadeScript>().FadeOut();
        yield return new WaitForSeconds(GetComponent<FadeScript>().fadeDuration);
        //quando acabar a animação de morte
        SceneManager.LoadScene(PlayerStats.SceneRespawn);
    }
    
    
    #endregion
    
    

    #region Pulando

    //checagem se encostou no chao
    private void IsOnGround()
    {
        if (Physics2D.OverlapCircle(groundCheck.position,0.15f,LayerMask.GetMask("Ground")))
        {
            jumpTimer = 0;
            coyoteTimer = 0;
            canJump = true;
            isJumping = false;
            rb.sharedMaterial = null;
            animator.SetBool("Falling",false);
            print("ss");
        }
        else
        {
            print("nn");
            if (!isJumping)
            {
                OnCoyote();
                
                if (coyoteTimer > 0.15f)
                {
                    canJump = false;
                    OnLanding();
                    OnFall();    
                }

            }
        }
        
    }

    private void OnCoyote()
    {
        coyoteTimer += Time.deltaTime;
    }
    
    void Jumping()
    {
       
        
        //primeiro checa se já apertou o botão de pulo
        if (inputC.Player.Jump.IsInProgress() && canJump && !healing)
        {
            //checa se já está pulando por mais de 0.5 seg
            //se sim, a gravidade volta ao normal e cmc a animação de queda
            if (jumpTimer >= jumpHolding)
            {
                isJumping = false;
                rb.gravityScale = gravity;
                canJump = false;
                jumpTimer = 0;
                OnFall();
            }
            else
            {
                isJumping = true;
                //ao apertar espaço a gravidade é 0
                //adiciona força no player pra cima, por linearVelocity
                //enquanto o player segurar espaço, por até 0.5 segundos
                rb.gravityScale = 1;
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
        if (!Attack.coolDown && !healing && !PlayerStats.TravarPlayer)
        {
            Attack.Atack(lastInput);
            OnAttackTrigger();
        }
    }
    
    private void OnSpecial(InputAction.CallbackContext obj)
    {
        //se tiver desbloqueado uma magia e apertar o botão de magia e tiver mana suficiente
        
        if ( PlayerStats.Magic != null &&
             PlayerStats.Magic.GetComponent<BaseMagic>().manaCost <= PlayerStats.PlayerMana && 
             !PlayerStats.MagicCoolDown)
        {
            //lance a magia q vc escolheu e gaste a mana q vc tinha
            //ativando uma função abstrata q ativa os efeitos especificos da magia
            
            GameObject magia = Instantiate(PlayerStats.Magic, transform.position,PlayerStats.Magic.transform.rotation );
            magia.GetComponent<BaseMagic>().ApplyMagicEffect(lastInput.x);
            PlayerReactions playerReac = new PlayerReactions();
            playerReac.OnManaCost(magia.GetComponent<BaseMagic>().manaCost);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void OnHeal()
    {
        PlayerReactions pr = new PlayerReactions();
        
        //cmc um timer q aumenta enquanto o botão de cura estiver precionado
        //enquanto isso o player estara tocando uma animação, n pode se mexer e tera a mana drenada
        //se o timer terminar ele ira se curar e a animação acabarar
        //se ele soltar no meio do timer ele n se curarar, mas a animação ira acabar
        if (healing)
        {
            if (healingTime >= PlayerStats.HealingTime)
            {
                healing = false;
                _spriteRenderer.color = Color.white;
                healingTime = 0;
            }
            else if(inputC.Player.Heal.IsInProgress())
            {
                animator.SetBool(Running, false);
                healingTime += Time.deltaTime;
                _spriteRenderer.color = new Color(76, 255, 231);
                float manaCost = PlayerStats.HealingCost / PlayerStats.HealingTime * Time.deltaTime;
                pr.OnManaCost(manaCost);
                pr.OnHeal(PlayerStats.HealingEffectiveness /PlayerStats.HealingTime * Time.deltaTime);

            }
            else
            {
                healing = false;
                _spriteRenderer.color = Color.white;
                healingTime = 0;


            }
        }

        
        //só corrigindo a mana se tiver decimal quebrado
        if (!healing)
        {
            PlayerStats.PlayerHp = Mathf.RoundToInt(PlayerStats.PlayerHp);
            PlayerStats.PlayerMana = Mathf.RoundToInt(PlayerStats.PlayerMana);
            GameObject.FindWithTag("UI").GetComponent<UIManager>().TxtManaMudar();
        }
    }
    
    void OnHealStart(InputAction.CallbackContext obj)
    {
        
        if (PlayerStats.PlayerMana >= PlayerStats.HealingCost)
        {
            healing = true;
        }
    }

    //cancelar a cura caso o player toma algum tipo de dano
    public void HealingCancelOnDamage()
    {
        healing = false;
        healingTime = 0;
        _spriteRenderer.color = Color.white;
    }

    #endregion


    private void OnMenu(InputAction.CallbackContext obj)
    {
        GameObject.Find("Canvas").GetComponent<UIManagerNOPlayer>().OpenPauseMenu();
        PlayerStats.TravarPlayer = true;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        //ao encostar no chão
        //reseta as variáveis do pulo e termina a animação de queda
        if (other.gameObject.GetComponent<Rigidbody2D>()&& other.gameObject.GetComponent<Rigidbody2D>().sharedMaterial == air)
        {
            rb.sharedMaterial = air;
        }
    }
}