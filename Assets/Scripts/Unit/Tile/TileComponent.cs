using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileComponent : UnitComponent
{
    public TileMovementType type;
    public static TileComponent SelectedTile = null;

    public Vector2 tileIndex = Vector2.zero;
    public BoxCollider boxCollider;
    public GameLevel level;

    public TileData tileInformation;

    #region Event Manager
    private List<EventTrigger> eventsOnTile = new List<EventTrigger>();

    public void AddEvent(EventTrigger evt)
    {
        eventsOnTile.Add(evt);
    }

    #endregion
    private CharacterComponent CharacterOnTile;


    public CharacterComponent characterOnTile
    {
        get => CharacterOnTile;
    }

    #region Selection

    public override void Select()
    {
        if (CharacterComponent.SelectedCharacter == null) return;
        if (CharacterComponent.SelectedCharacter.IsMoving || CharacterComponent.SelectedCharacter.IsAttacking) return;
        if (CharacterComponent.SelectedCharacter == this.characterOnTile) return;
        if (CharacterComponent.SelectedCharacter.characterInformation.aiControlled) return;
        if (SelectedTile != null) SelectedTile.Unselect();
        base.Select();
        if (CharacterComponent.SelectedCharacter.CanMove(this))
            SetHighlight(true, new Color(1.2f, 1.2f, 1.2f));
        else
            SetHighlight(true, Color.red);
        SelectedTile = this;
        gameManager.interfaceUpdater.UpdateInterface();
    }

    public override void Unselect()
    {
        base.Unselect();
        SetHighlight(false);
        SelectedTile = null;
        gameManager.interfaceUpdater.UpdateInterface();
    }
    #endregion

    public bool Occupied()
    {
        return characterOnTile != null;
    }

    public void AttachCharacter(CharacterComponent characterSet)
    {
        if (this.Occupied()) return;
        characterSet.attachedTile = this;
        CharacterOnTile = characterSet;

        //TriggerPossibleEvents
        foreach (EventTrigger t in eventsOnTile)
        {
            t.Trigger(characterSet, gameManager);
        }
    }

    public void RemoveCharacter()
    {
        if (!this.Occupied()) return;
        CharacterOnTile.attachedTile = null;
        CharacterOnTile = null;

        //DeactivatePossibleEvents
        foreach (EventTrigger t in eventsOnTile)
        {
            t.Deactivate();
        }
    }
}
