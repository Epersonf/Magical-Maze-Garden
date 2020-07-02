using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SnowMan : MonoBehaviour
{
    public Animator handAnimator;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnAttackEvent()
    {
        handAnimator.SetTrigger("attack");
    }

    void DealDamage(float damage)
    {
        anim.SetTrigger("lose");
    }
}
