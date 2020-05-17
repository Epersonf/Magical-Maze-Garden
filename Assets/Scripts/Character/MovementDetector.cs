using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDetector : MonoBehaviour
{
    [SerializeField]
    Transform MoveDetector;
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
        HighlightGroundAhead();
    }

    #region GetInfo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (MoveDetector != null)
            Gizmos.DrawWireCube(MoveDetector.position, Vector3.one * range);
        Gizmos.color = Color.green;
        if (AttackRange != null)
            Gizmos.DrawWireSphere(AttackRange.position, range);
    }

    public GroundComponent GetGroundAhead()
    {
        Collider[] colliders = Physics.OverlapSphere(MoveDetector.position, range);
        foreach (Collider c in colliders) {
            GroundComponent groundComponent = c.GetComponent<GroundComponent>();
            if (groundComponent != null)
                return groundComponent;
        }
        return null;
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
        currentHighlighted.SetHighlight(false);
        currentHighlighted = groundAhead;
        currentHighlighted.SetHighlight(true);
    }

    #endregion
}
