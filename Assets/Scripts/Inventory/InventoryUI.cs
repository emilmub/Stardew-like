using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    public InventorySlot[] slots;

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
            if (clickedSlot.currentItem.IsNull() && inventory.items[clickedIndex].IsNull())
            {
                inventory.items[clickedIndex] = new InventoryItem(draggedItem);
                draggedFromIndex = -1;

                dragIcon.enabled = false;
                dragIcon.sprite = null;

                draggedItem.Clear();

                Debug.Log("Dropped item in empty slot");
                UpdateUI();
                
            }
        }
    }
}
