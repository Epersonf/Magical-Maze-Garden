using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MovementDetector : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]
    Transform MoveDetector;
    [SerializeField]
    Transform ShootSpawn;
    [SerializeField]
    Transform AttackRange;
    [SerializeField]
    float range = 0.2f;

    LineRenderer lineRenderer;

    protected virtual void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.SetPosition(0, ShootSpawn.localPosition);
        lineRenderer.SetPosition(1, AttackRange.localPosition);
        lineRenderer.enabled = false;
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    #region GetInfo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (MoveDetector != null)
            Gizmos.DrawWireCube(MoveDetector.position, MoveDetector.localScale);
        Gizmos.color = Color.green;
        if (AttackRange != null)
            Gizmos.DrawWireSphere(AttackRange.position, range);
        Gizmos.color = Color.blue;
        if (ShootSpawn != null)
            Gizmos.DrawWireSphere(ShootSpawn.position, range);
    }

    public GroundComponent GetGroundAhead()
    {
        Collider[] colliders = Physics.OverlapBox(MoveDetector.position, MoveDetector.localScale);
        foreach (Collider c in colliders) {
            GroundComponent groundComponent = c.GetComponent<GroundComponent>();
            if (groundComponent != null)
                return groundComponent;
        }
        return null;
    }

    public Vector3 GetMagicBallOrigin()
    {
        return ShootSpawn.position;
    }

    public Vector3 GetMagicBallDestination()
    {
        return AttackRange.position;
    }
    #endregion

    #region HightlightGround
    GroundComponent currentHighlighted = null;

    public void HighlightGroundAhead()
    {
        GroundComponent groundAhead = GetGroundAhead();
        if (currentHighlighted == groundAhead) return;
        if (currentHighlighted != null)
            currentHighlighted.SetHighlight(false);
        if (groundAhead == null) return;
        currentHighlighted = groundAhead;
        if (currentHighlighted.IsOccupied())
            currentHighlighted.SetHighlight(true, Color.red);
        else
            currentHighlighted.SetHighlight(true);
        lineRenderer.enabled = true;
    }


    public void UnhighlightGroundAhead()
    {
        if (currentHighlighted == null) return;
        currentHighlighted.SetHighlight(false);
        currentHighlighted = null;
        lineRenderer.enabled = false;
    }
    #endregion
}
