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
    [SerializeField] InventoryUI hotBarUI;
    [SerializeField] private GameObject hotBarPanel;

    private TileManager tileManagerScript;
    private Inventory inventory;

    [SerializeField] private InventoryItem testItem;

    private int selectedIndex;
    // Start is called before the first frame update
    void Start()
    {
        tileManagerScript = tileManager.GetComponent<TileManager>();
        inventory = GetComponent<Inventory>();
        selectedIndex = 0;
        SelectItem(selectedIndex);
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

    void SelectItem(int index)
    {

        for (int i = 0; i < hotBarUI.slots.Length; ++i)
        {
            hotBarUI.slots[i].SetBorderColor(Color.yellow);
        }
        
        selectedIndex = index;
        hotBarUI.slots[selectedIndex].SetBorderColor(Color.red);


        InventoryItem selectedItem = hotBarUI.slots[selectedIndex].currentItem;
        if (!selectedItem.IsNull())
        {
            Debug.Log("Selected: " + selectedItem.itemData.name);
        }
        else
        {
            Debug.Log("Selected: Empty Slot");
        }
    }

    void HotBarSelection()
    {
        for (int i = 0; i < 9; ++i)
        {
            if (Input.GetKeyDown((KeyCode)((int)KeyCode.Alpha1 + i)))
            {
                SelectItem(i);
                break;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                SelectItem(9);
                break;
            }
        }
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0.0f)
        {
            selectedIndex += scroll > 0 ? 1 : scroll < 0 ? -1 : 0;
            if (selectedIndex >= 10)
            {
                selectedIndex %= 10;
            }
            else if (selectedIndex < 0) 
            {
                selectedIndex += 10;
            }
            
            SelectItem(selectedIndex);
        }
    }

    public void SwitchInventory()
    {
        inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
        hotBarPanel.SetActive(!hotBarPanel.activeSelf);
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
            HotBarSelection();
        }
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchInventory();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            inventory.AddItem(testItem.itemData, 1);
        }

        
    }

    
}
