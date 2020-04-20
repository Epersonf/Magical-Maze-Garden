using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : DataStructure
{
    public EventConditionChecker eventManager;
    public bool availableOnlyForTheMainTeam = true;
    [Tooltip("Max time that this can be activated, 0 = infite.")]
    public int maxActivations = 1;

    private int activations = 0;
    public void Generate(TileComponent tile)
    {
        activations = 0;
        tile.AddEvent(this);
    }

    public bool CanActivate(CharacterComponent character, GameManager manager)
    {
        if (availableOnlyForTheMainTeam && character.characterInformation.type != manager.currentLevel.mainTeam) return false;
        if (activations >= maxActivations && maxActivations > 0) return false;
        return true;
    }

    public void Trigger(CharacterComponent character, GameManager manager)
    {
        if (!CanActivate(character, manager)) return;
        eventManager.TryToGiveKeys(manager.currentLevel.keyObject.keyManager);
        Activate();
    }

    public virtual void Activate()
    {
        activations++;
    }

    public virtual void Deactivate()
    {

    }
}
