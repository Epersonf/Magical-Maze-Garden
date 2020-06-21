using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserShootComponent : ShootComponent
{
    LineRenderer lineRenderer;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    public float startWidth = .1f;
    public float endWidth = .2f;
    public float delayToDestroy = 1f;

    public override void SetDestination(Vector3 destination, ShooterCharacter emissor)
    {
        base.SetDestination(destination, emissor);
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, destination);

        AliveCreature aliveCreature = GetAttackedPlayer(transform.position, destination);
        if (aliveCreature) aliveCreature.DealDamage(emissor.damage);

        Invoke("EndEmissorAction", delayToDestroy);
    }

    public AliveCreature GetAttackedPlayer(Vector3 origin, Vector3 destination)
    {
        RaycastHit[] hit = Physics.RaycastAll(origin, destination - origin, Vector3.Distance(origin, destination));
        foreach (RaycastHit h in hit)
        {
            ShooterCharacter enemyShooter = h.transform.GetComponent<ShooterCharacter>();
            if (enemyShooter == null) continue;
            if (enemyShooter.team == emissor.team || !enemyShooter.enabled) continue;
            return h.transform.GetComponent<AliveCreature>();
        }
        return null;
    }
}
