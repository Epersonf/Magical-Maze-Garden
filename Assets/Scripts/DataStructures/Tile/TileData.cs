using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objects Data/TileData")]
public class TileData : DataStructure
{
    public string tileName;
    public GameObject prefab;

    public TileComponent Create(GameLevel levelSet, Vector2 tileSet, TileMovementType type)
    {
        GameManager gameManager = levelSet.gameManager;
        Vector3 position = new Vector3(tileSet.x * gameManager.gridSize, gameManager.high, tileSet.y * gameManager.gridSize);
        GameObject obj =  Instantiate(prefab, position, Quaternion.Euler(0, 0, 0));
        TileComponent tileComp = obj.AddComponent(typeof(TileComponent)) as TileComponent;
        tileComp.boxCollider = obj.AddComponent(typeof(BoxCollider)) as BoxCollider;
        tileComp.boxCollider.size = new Vector3(obj.transform.localScale.x * 8, tileComp.gameManager.gridSize * 5 * (int)type, obj.transform.localScale.z * 8);
        tileComp.type = type;
        tileComp.level = levelSet;
        tileComp.tileInformation = this;
        tileComp.tileIndex = tileSet;
        return tileComp;
    }
}

public enum TileMovementType
{
    Passable, Jumpable, Impassable
}
