using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ShootComponent : MonoBehaviour
{
    private Vector3 destination;
    private float speed = 5f;
    private ShooterCharacter emissor;
    public float margin = 0.4f;

    public void SetDestination(Vector3 destination, ShooterCharacter emissor)
    {
        this.destination = destination;
        this.emissor = emissor;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, destination) < margin)
            End();
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SendMessage("DealDamage", emissor.damage);
        End();
    }

    public void End()
    {
        Destroy(this.gameObject);
        emissor.gameObject.SendMessage("EndAction");
    }
}
