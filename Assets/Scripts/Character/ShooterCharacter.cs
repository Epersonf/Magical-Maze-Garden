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

    private void Awake()
    {
        emissor = GetComponent<CharacterComponent>();
        EndTurn += emissor.EndAction;
    }

    public void SpawnShoot(Vector3 origin, Vector3 destination)
    {
        StartCoroutine(SpawnShoot(origin, destination, delay));
    }

    public IEnumerator SpawnShoot(Vector3 origin, Vector3 destination, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject shoot = Instantiate(prefab, origin, Quaternion.identity);
        shoot.GetComponent<ShootComponent>().SetDestination(damage, speed, destination, EndTurn);
    }

}
