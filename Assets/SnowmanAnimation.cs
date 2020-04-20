using System;
using UnityEngine;

public class SnowmanAnimation : MonoBehaviour
{

    CharacterComponent characterComponent;
    public Animator animator;

    void Start()
    {
        characterComponent = GetComponent<CharacterComponent>();
        characterComponent.changeLifeEvent += TryToDecreaseBall;
    }

    public void TryToDecreaseBall()
    {
        if (characterComponent.life <= 1)
            animator.SetTrigger("lose");
    }

    
}
