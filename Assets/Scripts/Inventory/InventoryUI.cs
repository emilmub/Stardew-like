using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    public InventorySlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        slots = GetComponentsInChildren<InventorySlot>();
        UpdateUI();
    }

    // Update is called once per frame
    public void UpdateUI()
    {
        // Goes over the slots in the inventory (or Hotbar)
        for (int i = 0; i < slots.Length; i++) 
        {
            // Checks if we are over the amount of items in the inventory
            if (i < inventory.items.Count)
            {
                slots[i].Set(inventory.items[i]);
            }
            else
            {
                slots[i].Clear();
            }
        }
    }
}
