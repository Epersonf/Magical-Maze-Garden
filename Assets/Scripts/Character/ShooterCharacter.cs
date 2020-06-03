using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterComponent))]
public class ShooterCharacter : MonoBehaviour
{
    CharacterComponent emissor;
    public GameObject prefab;
    public int damage = 1;
    public float speed = 5f;
    public float delay = 0.3f;
    public Action EndTurn;
    public bool ranged = true;

    private void Awake()
    {
        emissor = GetComponent<CharacterComponent>();
        EndTurn += emissor.EndAction;
    }

    public void SpawnShoot(Vector3 origin, Vector3 destination, GroundComponent groundAhead)
    {
        if (ranged)
            StartCoroutine(SpawnShoot(origin, destination, delay));
        else
            StartCoroutine(HitAhead(groundAhead.attachedCharacter, delay));
    }

    public IEnumerator HitAhead(CharacterComponent characterComponent, float delay)
    {
        yield return new WaitForSeconds(delay);
        characterComponent.DealDamage(damage);
        EndTurn();
    }

    public IEnumerator SpawnShoot(Vector3 origin, Vector3 destination, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject shoot = Instantiate(prefab, origin, Quaternion.identity);
        shoot.GetComponent<ShootComponent>().SetDestination(damage, speed, destination, EndTurn);
    }

}
