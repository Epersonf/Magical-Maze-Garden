using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class OverGroundComponent : MonoBehaviour
{
    public GroundType overwriteType = GroundType.Impassable;

    private void OnCollisionEnter(Collision collision)
    {
        GroundComponent ground = collision.transform.GetComponent<GroundComponent>();
        if (ground == null) return;
        ground.groundType = overwriteType;
    }
}
