using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class GroundComponent : SelectableUnit
{
    public GroundType groundType;
    public Transform characterPosition;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (characterPosition != null)
            Gizmos.DrawWireSphere(characterPosition.position, 0.2f);
    }

    #region Occupied
    private TurnManager AttachedCharacter;
    public TurnManager attachedCharacter
    {
        get => AttachedCharacter;
    }

    public void AttachCharacter(TurnManager character)
    {
        AttachedCharacter = character;
    }

    public void DettachCharacter()
    {
        AttachedCharacter = null;
    }

    public bool IsOccupied()
    {
        return attachedCharacter != null || groundType == GroundType.Impassable;
    }
    #endregion
}

public enum GroundType
{
    Passable,
    Impassable
}
