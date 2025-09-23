using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolManager : MonoBehaviour
{
    public static ToolManager instance;

    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private GameObject tileManager;
    [SerializeField] private TypedTile tilledSoil;
    private TileManager tileManagerScript;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        tileManagerScript = tileManager.GetComponent<TileManager>();
    }

    // Update is called once per frame
    public void UseTool(ToolData tool)
    {
        switch (tool.toolType)
        {
            case ToolData.ToolType.Axe:
                Debug.Log("Using axe with power {power}");
                break;
            case ToolData.ToolType.Hoe:
                Till();
                break;
            case ToolData.ToolType.Pickaxe:
                Debug.Log("Using pickaxe with power {power}");
                break;
            case ToolData.ToolType.WateringCan:
                Debug.Log("Using watering can with power {power}");
                break;
        }
    }

    Vector3Int MouseToCell()
    {
        // Transform mouse coordinates to tilemap coordinates
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = 0.0f;
        return groundTilemap.WorldToCell(worldPoint);
    }
    void Till()
    {
        // Function for tilling untilled soil

        // Detect which cell was clicked
        // TODO: Limit distance for tilling soil only to cells immidiate to the player
        Vector3Int clickedCell = MouseToCell();
        TileBase clickedTile = groundTilemap.GetTile(clickedCell);
        TypedTile tile = clickedTile as TypedTile;
        TileData tileData = tileManagerScript.GetOrCreateTileData(clickedCell, tile.tileType);
        // Only till the soil if it is tillable
        if (tileData.isTillable)
        {
            // Change soil attributes to tilled
            tileData.isTilled = true;
            tileData.isTillable = false;
            tileData.isPlantable = true;
            groundTilemap.SetTile(clickedCell, tilledSoil); // Change the tile clicked to be tilled
        }
        else
        {
            Debug.Log("Tile is not tillable");
        }
    }

}
