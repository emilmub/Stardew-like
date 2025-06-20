using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int quantity;

    public InventoryItem(ItemData data, int qty = 1)
    {
        itemData = data;
        quantity = qty;
    }

    public InventoryItem(InventoryItem item)
    {
        itemData = item.itemData;
        quantity = item.quantity;
    }

    public void Clear()
    { 
        itemData = null;
        quantity = 0; 
    }

    public bool IsNull()
    {
        return itemData == null || quantity == 0;
    }
}
