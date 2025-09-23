using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;    

    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] InventoryUI hotBarUI;
    [SerializeField] private GameObject hotBarPanel;
    
    private Inventory inventory;

    [SerializeField] private InventoryItem testItem;

    private int selectedIndex;
    private InventoryItem selectedItem;

    [SerializeField] private ToolManager toolManager;
    // Start is called before the first frame update
    void Start()
    {
        
        inventory = GetComponent<Inventory>();
        selectedIndex = 0;
        SelectItem(selectedIndex);
    }

    

    void Move()
    {
        // Player movement on the screen
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movementVector = new Vector3(horizontalInput, verticalInput, 0);
        movementVector *= movementSpeed * Time.deltaTime;
        transform.position += movementVector;
    }

    void SelectItem(int index)
    {
        // Select Item in hotbar

        // Cycle through the spaces in the hotbar and change the bordercolor
        for (int i = 0; i < hotBarUI.slots.Length; ++i)
        {
            hotBarUI.slots[i].SetBorderColor(Color.yellow);
        }
        
        // Change the selected item bordercolor
        selectedIndex = index;
        hotBarUI.slots[selectedIndex].SetBorderColor(Color.red);


        selectedItem = hotBarUI.slots[selectedIndex].currentItem;
        if (!selectedItem.IsNull())
        {
            // TODO: Return (or set in a member variable, or something) the selected item for the player to use
            Debug.Log("Selected: " + selectedItem.itemData.name);
        }
        else
        {
            //TODO: Set the selected item to null or something like that
            Debug.Log("Selected: Empty Slot");
        }
    }

    void HotBarSelection()
    {
        // Controller for selecting an item in the hotbar

        // Check if a number key has been pressed and select the corresponding item
        for (int i = 0; i < 9; ++i)
        {
            if (Input.GetKeyDown((KeyCode)((int)KeyCode.Alpha1 + i)))
            {
                // Keys 1 through 9
                SelectItem(i);
                break;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                // Key 0
                SelectItem(9);
                break;
            }
        }

        // Select using the mousewheel
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0.0f)
        {
            // Limit the scroll value to 1, -1, or 0
            selectedIndex += scroll > 0 ? 1 : scroll < 0 ? -1 : 0;
            // Limit the index to be between 0 and 10
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
        // Open or close the inventory and the inverse for the hotbar
        inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
        hotBarPanel.SetActive(!hotBarPanel.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventoryCanvas.activeSelf)
        {
            // If the inventory is not open then we can interact with the world
            Move();
            // TODO: Make an UseItem function that changes with the item at hand
            // TODO: Make a better way to parse inputs so that it can be changed at runtime if needed
            if (Input.GetMouseButtonDown(0) && !selectedItem.IsNull())
            {
                selectedItem.itemData.Use();
            }
            HotBarSelection();
        }
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Open or close the inventory if TAB key is pressed
            SwitchInventory();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            // Function for testing inventory, can be used to test other things in the future
            inventory.AddItem(testItem.itemData, 1);
        }

        
    }

    
}
