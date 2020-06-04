using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TurnManager))]
public class ShooterCharacter : MonoBehaviour
{
    TurnManager turnManager;
    MovementDetector movementDetector;
    public int team = 0;
    public GameObject prefab;
    public int damage = 1;
    public float speed = 5f;
    public float delay = 0.3f;
    public bool ranged = true;


    private void Awake()
    {
        turnManager = GetComponent<TurnManager>();
        movementDetector = GetComponentInChildren<MovementDetector>();
    }

    public void Attack()
    {
        if (!turnManager.IsTurn() || turnManager.executingAction) return;
        if (ranged)
            StartCoroutine(SpawnShoot(movementDetector.GetMagicBallOrigin(), movementDetector.GetMagicBallDestination(), delay));
        else
            StartCoroutine(HitAhead(movementDetector.GetGroundAhead().attachedCharacter, delay));
    }

    public IEnumerator HitAhead(TurnManager characterComponent, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (characterComponent == null) yield break;
        characterComponent.gameObject.SendMessage("DealDamage", damage, SendMessageOptions.DontRequireReceiver);
        SendMessage("StartAction");
    }

    public IEnumerator SpawnShoot(Vector3 origin, Vector3 destination, float delay)
    {
        SendMessage("StartAction");
        yield return new WaitForSeconds(delay);
        GameObject shoot = Instantiate(prefab, origin, Quaternion.identity);
        shoot.GetComponent<ShootComponent>().SetDestination(destination, this);
    }
}
