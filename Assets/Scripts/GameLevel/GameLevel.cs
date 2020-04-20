using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameLevel : ScriptableObject
{
    [Tooltip("All tiles information.")]
    public TerrainObject terrainObject;

    [Space(20)]
    [Tooltip("The way to conclude the map.")]
    public EndLevelType endGame;

    [Tooltip("The team that should not die.")]
    public CharacterType mainTeam;

    [Space(20)]
    [Tooltip("The KeyObject stores all the keys of the GameLevel.")]
    public KeyObject keyObject;

    [Space(20)]
    [Tooltip("Contains information about every unit that can be attached to a tile.")]
    public OverTileObject overTileObject;
    

    [System.NonSerialized]
    public List<List<TileComponent>> tilesInGameRunning = new List<List<TileComponent>>();

    [System.NonSerialized]
    public List<CharacterComponent> charactersInGameRunning = new List<CharacterComponent>();

    #region World Generation
    public GameManager gameManager;
    public void Generate(GameManager gameManager)
    {
        this.gameManager = gameManager;
        tilesInGameRunning.Clear();
        int x = 0;
        int y;
        foreach (Column c in terrainObject.terrain.tiles)
        {
            y = 0;
            tilesInGameRunning.Add(new List<TileComponent>());
            foreach(GroundData t in c.columns)
            {
                TileComponent tileToAdd = t.tileBellow.Create(this, new Vector2(x, y), t.typeOfMovement);

                //add overtileunit
                if (t.overObjectPrefab == null) goto jumpInstantiateOvertile;

                GameObject overTile = MonoBehaviour.Instantiate(t.overObjectPrefab, tileToAdd.transform.position, Quaternion.identity);
                overTile.transform.parent = tileToAdd.transform;
                

                jumpInstantiateOvertile:
                tilesInGameRunning[x].Add(tileToAdd);

                //addCharacters
                foreach (CharacterSpawn charInfo in overTileObject.charactersInGame)
                {
                    if (charInfo.tile.x != x || charInfo.tile.y != y) continue;
                    CharacterComponent charToAdd = charInfo.characterObject.Generate(tileToAdd);
                    charactersInGameRunning.Add(charToAdd);
                }

                //addEvents
                foreach (EventSpawn eventInfo in overTileObject.eventsInGame)
                {
                    if (eventInfo.tile.x != x || eventInfo.tile.y != y) continue;
                    eventInfo.eventObject.Generate(tileToAdd);
                }
                y++;
            }
            x++;
        }
    }

    public TileComponent GetCameraPos()
    {
        int x = tilesInGameRunning.Count / 2;
        int y = tilesInGameRunning[x].Count / 2;
        return tilesInGameRunning[x][y];
    }
    #endregion

    #region Util
    public bool HasOnlyOneTeam()
    {
        CharacterType? type = null;
        foreach (CharacterComponent c in charactersInGameRunning)
        {
            if (type == null) type = c.characterInformation.type;
            else if (type != c.characterInformation.type) return false;
        }
        return true;
    }

    public bool HasTeam(CharacterType type)
    {
        foreach (CharacterComponent c in charactersInGameRunning)
        {
            if (c.characterInformation.type == type)
                return true;
        }
        return false;
    }

    public bool Won()
    {
        bool achievedAll = keyObject.conditionsToEnd.HasAchievedAllKeys(keyObject.keyManager);
        bool hasOnlyATeam = HasOnlyOneTeam();
        if (achievedAll && !keyObject.killAllPlayersNeeded) return true;
        if (hasOnlyATeam && keyObject.killAllPlayersNeeded && achievedAll) return true;
        return false;
    }

    public bool Lost()
    {
        if ((endGame == EndLevelType.Deathmatch && HasOnlyOneTeam()) ||
            (endGame == EndLevelType.Variables && !HasTeam(mainTeam)))
        {
            return true;
        }
        return false;
    }
    #endregion
}