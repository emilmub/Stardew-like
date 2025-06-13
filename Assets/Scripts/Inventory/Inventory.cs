using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxSlots = 30;
    public List<InventoryItem> items = new List<InventoryItem>();
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] InventoryUI hotBarUI;

    public bool AddItem(ItemData data, int amount = 1)
    {
        // If the item is stackable, look if we have it in the inventory
        // and add the max amount allowed
        if (data.isStackable)
        {
            InventoryItem existing = items.FindLast(i => i.itemData == data);
            if (existing != null)
            {
                int space = data.maxStackSize - existing.quantity;
                int addAmount = Mathf.Min(space, amount);
                existing.quantity += addAmount;
                amount -= addAmount;
            }
        }

        // If the item was not stackable or there is a remaining amount of the stackable item,
        // Add a new item to the list with the max amount allowed
        while (amount > 0 && items.Count < maxSlots)
        {
            int addAmount = Mathf.Min(data.maxStackSize, amount);
            items.Add(new InventoryItem(data, addAmount));
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
                    items.RemoveAt(i);
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
