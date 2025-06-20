using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    public InventorySlot[] slots;
    public InventoryPanelType panelType;
    public Image dragIcon;
    private InventoryItem draggedItem = new InventoryItem(null, 0);
    private int draggedFromIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        slots = GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotIndex = i;
        }
        UpdateUI();
    }

    private void Update()
    {
        // If we are dragging an item, make its icon follow the mouse
        if (!draggedItem.IsNull())
        {
            dragIcon.transform.position = Input.mousePosition;
        }

        // If we click outside of the inventory panel (excluding the hotbarpanel)
        if (panelType != InventoryPanelType.Hotbar && Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (!draggedItem.IsNull())
                {
                    Debug.Log("Throw object outside of inventory");
                }
                // TODO: Should I close the inventory if the user clicks outside of the inventory without an object?
            }
        }
    }

    // Update is called once per frame
    public void UpdateUI()
    {
        // Goes over the slots in the inventory (or Hotbar)
        for (int i = 0; i < slots.Length; i++) 
        {
            // Checks if we are over the amount of items in the inventory
            if (!inventory.items[i].IsNull())
            {
                slots[i].Set(inventory.items[i]);
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    public void OnSlotClicked(InventorySlot clickedSlot)
    {
        int clickedIndex = clickedSlot.slotIndex;
        
        // We don't want this functionality with the hotbar
        if (panelType == InventoryPanelType.Hotbar) return;

        // If we did not have an item before clicking
        if (draggedItem.IsNull())
        {
            // If the clicked slot contains an item
            if ( (!clickedSlot.currentItem.IsNull()) && (!inventory.items[clickedIndex].IsNull()) )
            {
                draggedItem = new InventoryItem(clickedSlot.currentItem);
                draggedFromIndex = clickedIndex;

                dragIcon.enabled = true;
                dragIcon.sprite = draggedItem.itemData.icon;

                inventory.items[clickedIndex].Clear();
                Debug.Log("Clicked a valid object");
                UpdateUI();
            }
        }
        // If we are already dragging an item
        else
        {
            // If we drop the item into an empty slot
            if (clickedSlot.currentItem.IsNull() && inventory.items[clickedIndex].IsNull())
            {

                inventory.items[clickedIndex] = new InventoryItem(draggedItem); // drop the item
                draggedFromIndex = -1; // Reset the index

                // Reset the dragged icon
                dragIcon.enabled = false;
                dragIcon.sprite = null;

                // Reset the dragged item
                draggedItem.Clear();

                Debug.Log("Dropped item in empty slot");
                UpdateUI();
                
            }
            // If we drop the item into an occupied slot
            else if (!clickedSlot.currentItem.IsNull() && !inventory.items[clickedIndex].IsNull())
            {
                InventoryItem tempItem = clickedSlot.currentItem;

                inventory.items[clickedIndex] = new InventoryItem(draggedItem);

                draggedItem = new InventoryItem(clickedSlot.currentItem);

                dragIcon.sprite = draggedItem.itemData.icon;

                Debug.Log("Dropped item in occupied slot");
                UpdateUI();
            }
            // TODO: What happens if we drop outside the inventory panel
        }
    }
}
