using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameLevelObject/Terrain")]
public class TerrainObject : ScriptableObject
{
    [Tooltip("All game tiles and map information.")]
    public Terrain terrain;
}

[System.Serializable]
public class Terrain
{
    public Column[] tiles;
}

[System.Serializable]
public class Column
{
    public GroundData[] columns;
}

[System.Serializable]
public class GroundData
{
    public TileData tileBellow;
    public GameObject overObjectPrefab;
    public TileMovementType typeOfMovement;
}