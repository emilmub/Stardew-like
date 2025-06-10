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
}
