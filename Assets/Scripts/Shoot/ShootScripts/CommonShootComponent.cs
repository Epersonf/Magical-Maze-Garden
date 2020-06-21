using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class CommonShootComponent : ShootComponent
{
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, destination) < margin)
            EndEmissorAction();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ShooterCharacter shooter = collision.gameObject.GetComponent<ShooterCharacter>();
        if (shooter)
            if (emissor.team != shooter.team)
                collision.gameObject.SendMessage("DealDamage", emissor.damage, SendMessageOptions.DontRequireReceiver);
        EndEmissorAction();
    }
}
