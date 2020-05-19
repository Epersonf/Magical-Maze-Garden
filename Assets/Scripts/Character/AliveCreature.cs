using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AliveCreature : MonoBehaviour
{
    private bool Died = false;
    public bool died
    {
        get => Died;
    }

    private int Life;
    public int maxLife = 1;

    protected Animator animator;

    #region Inherited functions
    protected virtual void Awake()
    {
        Life = maxLife;
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {

    }
    
    protected virtual void Update()
    {

    }
    #endregion

    public void DealDamage(int damage)
    {
        Life -= damage;
        Life = Mathf.Clamp(Life, 0, damage);
        if (Life <= 0) Die();
    }

    public void Heal(int heal)
    {
        DealDamage(-heal);
    }

    public virtual void Die()
    {
        Died = true;
    }
}
