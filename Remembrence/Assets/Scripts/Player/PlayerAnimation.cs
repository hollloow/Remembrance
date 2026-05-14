using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected void OnAttackTrigger()
    {
        animator.SetTrigger("Attacking");
    }

    protected void OnRunning()
    {
        
    }
  
}
