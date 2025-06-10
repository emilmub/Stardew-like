using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private GameObject tileManager;

    [SerializeField] private GameObject inventoryCanvas;

    private TileManager tileManagerScript;
    private Inventory inventory;

    [SerializeField] private InventoryItem testItem;
    // Start is called before the first frame update
    void Start()
    {
        tileManagerScript = tileManager.GetComponent<TileManager>();
        inventory = GetComponent<Inventory>();
    }

    Vector3Int MouseToCell()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = 0.0f;
        return groundTilemap.WorldToCell(worldPoint);
    }

    void Move()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movementVector = new Vector3(horizontalInput, verticalInput, 0);
        movementVector *= movementSpeed * Time.deltaTime;
        transform.position += movementVector;
    }

    void Till()
    {
        Vector3Int clickedCell = MouseToCell();
        TileData tileData = tileManagerScript.GetOrCreateTileData(clickedCell);
        if (tileData.isTillable)
        {
            tileData.isTilled = true;
            tileData.isTillable = false;
            tileData.isPlantable = true;
            Debug.Log("Tile has been tilled");
        }
        else
        {
            Debug.Log("Tile is not tillable");
        }
    }

    void Plant()
    {
        Vector3Int clickedCell = MouseToCell();
        TileData tileData = tileManagerScript.GetOrCreateTileData(clickedCell);
        if (tileData.isPlantable)
        {
            tileData.isPlantable = false;
            tileData.hasSeeds = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventoryCanvas.activeSelf)
        {
            Move();
            // TODO: Make an UseItem function that changes with the item at hand
            // TODO: Make a better way to parse inputs so that it can be changed at runtime if needed
            if (Input.GetMouseButtonDown(0))
            {
                Till();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            inventory.AddItem(testItem.itemData, 1);
        }


    }

    
}
