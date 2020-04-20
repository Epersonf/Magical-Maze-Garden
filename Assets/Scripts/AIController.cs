using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : PathfinderUtil
{
    public static void TakeAction(CharacterComponent character)
    {
        CharacterComponent target = FindClosestTarget(character);
        if (target == null)
        {
            TileComponent tileToMove = FindClosestTile(GetPossibleTiles(character), character);
            character.Move(tileToMove);
            return;
        }

        if (CanAttack(character, target))
        {
            character.LookAt(target.transform.position);
            character.Attack();
        }
        else
        {
            TileComponent tileToMove = FindClosestTile(GetPossibleTiles(character), target);
            character.Move(tileToMove);
        }
    }

    public static bool CanAttack(CharacterComponent character, CharacterComponent target)
    {
        if (!character.CanAttack()) return false;
        if (character.attachedTile.tileIndex.x != target.attachedTile.tileIndex.x && character.attachedTile.tileIndex.y != target.attachedTile.tileIndex.y)
            return false;
        if (TileDistance(character.attachedTile.tileIndex, target.attachedTile.tileIndex) > character.characterInformation.range)
            return false;
        return true;
    }

    public static int TileDistance(Vector2 a, Vector2 b)
    {
        Vector2 dif = a - b;
        return (int)(Mathf.Abs(dif.x) + Mathf.Abs(dif.y));
    }
}
