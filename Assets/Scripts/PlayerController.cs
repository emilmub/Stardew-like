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

    private TileManager tileManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        tileManagerScript = tileManager.GetComponent<TileManager>();
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
        Move();

        // TODO: Make an UseItem function that changes with the item at hand
        if (Input.GetMouseButtonDown(0))
        {
            Till();
        }


    }

    
}
