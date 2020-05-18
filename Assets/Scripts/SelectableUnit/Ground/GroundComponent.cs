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
    private CharacterComponent AttachedCharacter;
    public CharacterComponent attachedCharacter
    {
        get => AttachedCharacter;
        set
        {
            AttachedCharacter = value;
        }
    }

    public bool IsOccupied()
    {
        return attachedCharacter != null;
    }
    #endregion
}

public enum GroundType
{
    Passable,
    Impassable,
    Jumpable,
    Ramp
}
