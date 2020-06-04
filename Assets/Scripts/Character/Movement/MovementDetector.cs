using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDetector : MonoBehaviour
{
    [SerializeField]
    Transform MoveDetector;
    [SerializeField]
    Transform ShootSpawn;
    [SerializeField]
    Transform AttackRange;
    [SerializeField]
    float range = 0.2f;

    protected virtual void Awake()
    {

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
    }

    public void Unhighlight()
    {
        if (currentHighlighted == null) return;
        currentHighlighted.SetHighlight(false);
        currentHighlighted = null;
    }
    #endregion
}
