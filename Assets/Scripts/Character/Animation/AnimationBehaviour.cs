using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBehaviour : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void OnDieEvent()
    {
        animator.SetTrigger("die");
    }

    public void OnAttackEvent()
    {
        animator.SetTrigger("attack");
    }

    public void OnMoveEvent()
    {
        animator.SetBool("moving", true);
    }

    public void OnIdleEvent()
    {
        animator.SetBool("moving", false);
    }
}
