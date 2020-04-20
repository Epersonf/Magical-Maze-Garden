using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameLevelObject/OverTileUnits")]
public class OverTileObject : ScriptableObject
{
    [Space(50)]
    [Tooltip("All the characters that will be spawned in order of turns and its position.")]
    public CharacterSpawn[] charactersInGame;

    [Space(50)]
    [Tooltip("All the events that will be spawned and its position.")]
    public EventSpawn[] eventsInGame;
}

#region Map generation
[System.Serializable]
public class CharacterSpawn
{
    public CharacterData characterObject;
    public Vector2 tile;
}
[System.Serializable]
public class EventSpawn
{
    public EventTrigger eventObject;
    public Vector2 tile;
}
#endregion
