using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //os parametros q estão sendo usados pelo animator
    protected static readonly int Attacking = Animator.StringToHash("Attacking");
    protected static readonly int Landing = Animator.StringToHash("Landing");
    protected static readonly int Dying = Animator.StringToHash("Dying");
    protected static readonly int Falling = Animator.StringToHash("Falling");
    protected static readonly int Running = Animator.StringToHash("Running");
    protected static readonly int Hurted = Animator.StringToHash("Hurted");
    
    protected Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected void OnRunning()
    {
       
        animator.SetBool("Running",true);
    }
    
    
    //funções q setam os parametros para as animações funcionarem 
    protected void OnAttackTrigger()
    {
        animator.SetTrigger(Attacking);
    }

    protected void OnJump()
    {
        animator.SetBool("Jumping",true);
    }

    protected void OnFall()
    {
        animator.SetBool("Falling",true);
        animator.SetBool("Jumping",false);
    }

    protected void OnLanding()
    {
        animator.SetBool("Falling",false);
    }

    public void OnHurt()
    {
       animator.SetTrigger(Hurted); 
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("Landing",true);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("Landing",false);
        }
    }
}
