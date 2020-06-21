using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    #region Inherited functions
    public void Awake()
    {
        Life = maxLife;
    }

    public void Start()
    {

    }
    
    public void Update()
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

    public void Die()
    {
        Died = true;
        gameObject.SendMessage("OnDieEvent", SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject, 3);
        GameManager.active.NextLevel();
        this.enabled = false;
    }
}
