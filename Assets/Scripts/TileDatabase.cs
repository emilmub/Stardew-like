using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "TileDatabase", menuName = "Tiles/Tile Database")]
public class TileDatabase : ScriptableObject
{
    public List<TileData> tileDataList;

    private Dictionary<string, TileData> tileDataMap;

    public TileData GetData(string tileType)
    {
        if (tileDataMap == null)
        {
            tileDataMap = tileDataList.ToDictionary(data => data.tileType);
        }

        tileDataMap.TryGetValue(tileType, out TileData data);
        return data;
    }

}
