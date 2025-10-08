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

    float TaxicabDistance(Vector3 vector)
    {
        float suma = 0;
        for(int i = 0; i < 3; i++) 
        {
            suma += Mathf.Abs(vector[i]);
        }
        return suma;
    }

    void DebugVector(Vector3 vector, string name)
    {
        Debug.Log(name + " x: " + vector.x + " y: " + vector.y + " z: " + vector.z);
    }

    void Till(int maxDistance = 2)
    {
        // Function for tilling untilled soil

        // Detect which cell was clicked

        // Limit distance for tilling soil only to cells within a taxicab radius to the player
        Vector3 vectorPlayerToMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vectorPlayerToMouse.z = 0.0f;
        vectorPlayerToMouse -= transform.position;
        float vectorDistance = TaxicabDistance(vectorPlayerToMouse);
        DebugVector(Camera.main.ScreenToWorldPoint(Input.mousePosition), "click");
        DebugVector(transform.position, "player");
        Debug.Log("Distance to player: " + vectorDistance);
        Vector3Int clickedCell = vectorDistance < maxDistance? MouseToCell() : groundTilemap.WorldToCell(Vector3.Normalize(vectorPlayerToMouse));

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
