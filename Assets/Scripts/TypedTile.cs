using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Typed Tile", menuName = "Tiles/Typed Tile")]
public class TypedTile : Tile
{
    public TileType tileType;
}
