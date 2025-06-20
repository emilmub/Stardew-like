using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxSlots = 30;
    public List<InventoryItem> items = new List<InventoryItem>();
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] InventoryUI hotBarUI;

    private void Awake()
    {
        while (items.Count < maxSlots)
        {
            items.Add(new InventoryItem(null, 0));
        }
    }

    public bool HasEmptySlot()
    {
        return items.Any(item => item.itemData == null || item.quantity == 0);
    }

    public bool AddItem(ItemData data, int amount = 1)
    {
        // If the item is stackable, look if we have it in the inventory
        // and add the max amount allowed
        if (data.isStackable)
        {
            List<InventoryItem> existing = items.FindAll(i => i.itemData == data);
            // If a match was found
            if (existing.Count > 0)
            {
                for (int i = 0; i < existing.Count; i++)
                {
                    if (amount > 0)
                    {
                        int space = data.maxStackSize - existing[i].quantity;
                        int addAmount = Mathf.Min(space, amount);
                        existing[i].quantity += addAmount;
                        amount -= addAmount;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        // If the item was not stackable or there is a remaining amount of the stackable item,
        // And there is still an empty spot in the inventory, then
        // Add a new item to the list with the max amount allowed
        while (amount > 0 && HasEmptySlot())
        {
            int addAmount = Mathf.Min(data.maxStackSize, amount);
            // Add amount to the first null element in the list
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].IsNull())
                {
                    items[i] = new InventoryItem(data, addAmount);
                    break;
                }
            }
            amount -= addAmount;
        }

        
        inventoryUI.UpdateUI();
        hotBarUI.UpdateUI();

        return amount == 0;
    }

    // TODO: Implement UpdateUI on this function
    public void RemoveItem(ItemData data, int amount = 1)
    {
        for (int i = items.Count - 1; i >= 0 && amount > 0; i--)
        {
            if (items[i].itemData == data)
            {
                int removeAmount = Mathf.Min(amount, items[i].quantity);
                items[i].quantity -= removeAmount;
                amount -= removeAmount;
                if (items[i].quantity <= 0)
                {
                    items[i] = null;
                }
            }
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
