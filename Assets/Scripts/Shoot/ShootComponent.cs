using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootComponent : MonoBehaviour
{
    protected Vector3 destination;
    protected float speed = 5f;
    protected ShooterCharacter emissor;
    public float margin = 0.4f;

    public virtual void SetDestination(Vector3 destination, ShooterCharacter emissor)
    {
        this.destination = destination;
        this.emissor = emissor;
    }

    protected virtual void EndEmissorAction()
    {
        Destroy(this.gameObject);
        emissor.gameObject.SendMessage("EndAction");
    }
}
