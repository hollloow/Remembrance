using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //os parametros q estão sendo usados pelo animator
    protected static readonly int Attacking = Animator.StringToHash("Attacking");
    protected static readonly int Dying = Animator.StringToHash("Dying");
    protected static readonly int Falling = Animator.StringToHash("Falling");
    protected static readonly int Running = Animator.StringToHash("Running");
    
    protected Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected void OnRunning(float direction)
    {
        //definindo a direção q o player tá
        if (direction > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (direction < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
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
}
