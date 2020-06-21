using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterCharacter : MonoBehaviour
{
    TurnManager turnManager;
    MovementDetector movementDetector;
    public int team = 0;
    public GameObject prefab;
    public int damage = 1;
    public float speed = 5f;
    public float delay = 0.3f;
    public float afterDelay = 0.3f;
    public bool ranged = true;


    private void Awake()
    {
        turnManager = GetComponent<TurnManager>();
        movementDetector = GetComponentInChildren<MovementDetector>();
    }

    public void Attack()
    {
        if (!turnManager.IsTurn() || turnManager.executingAction) return;
        SendMessage("OnAttackEvent", SendMessageOptions.DontRequireReceiver);
        SendMessage("StartAction");
        BroadcastMessage("UnhighlightGroundAhead");
        if (ranged)
            StartCoroutine(SpawnShoot(movementDetector.GetMagicBallOrigin(), movementDetector.GetMagicBallDestination(), delay));
        else
            StartCoroutine(HitAhead(movementDetector.GetGroundAhead().attachedCharacter, delay));
    }

    public IEnumerator HitAhead(TurnManager characterComponent, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (characterComponent && team != characterComponent.GetComponent<ShooterCharacter>().team)
        {
            characterComponent.gameObject.SendMessage("DealDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
        yield return new WaitForSeconds(afterDelay);
        SendMessage("EndAction");
    }

    public IEnumerator SpawnShoot(Vector3 origin, Vector3 destination, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject shoot = Instantiate(prefab, origin, transform.rotation);
        shoot.GetComponent<ShootComponent>().SetDestination(destination, this);
    }

    #region Events
    public void OnDieEvent()
    {
        this.enabled = false;
    }
    #endregion
}
