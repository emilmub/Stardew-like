using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTileData", menuName = "Tiles/Tile Data")]

public class TileData : ScriptableObject
{
    public string tileType;

    public bool isTillable;
    public bool isTilled;

    public bool isPlantable;

    public bool hasPlant;
    public bool hasSeeds;
}