using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderUtil
{
    public static TileComponent FindClosestTile(List<TileComponent> tileComponents, CharacterComponent target)
    {
        float distance = -1;
        TileComponent tileToReturn = null;
        if (tileComponents == null) return null;
        foreach (TileComponent tile in tileComponents)
        {
            float dist = Vector3.Distance(tile.transform.position, target.attachedTile.transform.position);
            if (dist < distance || distance < 0)
            {
                distance = dist;
                tileToReturn = tile;
            }
        }
        return tileToReturn;
    }

    public static CharacterComponent FindClosestTarget(CharacterComponent character)
    {
        float distance = -1;
        CharacterComponent target = null;
        foreach (CharacterComponent possibleTarget in character.gameManager.currentLevel.charactersInGameRunning)
        {
            if (possibleTarget.characterInformation.type == character.characterInformation.type) continue;
            if (!possibleTarget.alive) continue;
            float dist = Vector3.Distance(possibleTarget.attachedTile.transform.position, character.attachedTile.transform.position);
            if (dist < distance || distance < 0)
            {
                distance = dist;
                target = possibleTarget;
            }
        }
        return target;
    }

    public static List<TileComponent> GetPossibleTiles(CharacterComponent character)
    {
        List<TileComponent> adjacent = new List<TileComponent>();
        if (character.attachedTile == null) return null;
        Vector2 pos = character.attachedTile.tileIndex;
        int[] v = { 0, 1, 0, -1, 1, 0, -1, 0 };
        for (int i = 0; i < v.Length; i += 2)
        {
            try
            {
                TileComponent tileToAdd = character.gameManager.currentLevel.tilesInGameRunning[(int)pos.x + v[i]][(int)pos.y + v[i + 1]];
                if (!character.CanMove(tileToAdd)) continue;
                adjacent.Add(tileToAdd);
            }
            catch (System.ArgumentOutOfRangeException) { continue; }
        }
        return adjacent;
    }
}
