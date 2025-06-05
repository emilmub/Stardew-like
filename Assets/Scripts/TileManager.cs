using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<Vector3Int, TileData> tilesDataDict = new();

    public TileData GetOrCreateTileData(Vector3Int cellPosition)
    {
        if (!tilesDataDict.TryGetValue(cellPosition, out var data))
        {
            data = new TileData();
            tilesDataDict[cellPosition] = data;
        }
        return data;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
