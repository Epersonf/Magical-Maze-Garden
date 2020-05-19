using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ShootComponent : MonoBehaviour
{
    private Vector3 destination;
    private int damage = 1;
    private float speed = 5f;
    private Action EndTurn;
    public float margin = 0.4f;

    public void SetDestination(int damage, float speed, Vector3 destination, Action EndTurn)
    {
        this.damage = damage;
        this.speed = speed;
        this.destination = destination;
        this.EndTurn = EndTurn;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, destination) < margin)
            End();
    }

    private void OnCollisionEnter(Collision collision)
    {
        AliveCreature aliveCreature = collision.transform.GetComponent<AliveCreature>();
        if (aliveCreature != null)
            aliveCreature.DealDamage(damage);
        End();
    }

    public void End()
    {
        EndTurn();
        Destroy(this.gameObject);
    }
}
