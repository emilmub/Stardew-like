using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<TileData> tilesData = new List<TileData>();

    public Dictionary<Vector3Int, TileData> tilesDataDict = new();
    private Dictionary<string, TileData> tileTypeLookUp = new Dictionary<string, TileData>();

    public TileData GetOrCreateTileData(Vector3Int cellPosition, TileType tileType)
    {
        if (!tilesDataDict.TryGetValue(cellPosition, out var data))
        {
            data = Instantiate(tileTypeLookUp[tileType.ToString().ToLower()]);
            tilesDataDict[cellPosition] = data;
        }
        return data;
    }

    private void Awake()
    {
        foreach (var tileData in tilesData)
        {
            if (!string.IsNullOrEmpty(tileData.tileType))
            {
                tileTypeLookUp[tileData.tileType.ToLower()] = tileData;
            }
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
