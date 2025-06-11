using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI quantityText;
    public InventoryItem currentItem;
    

    public void Set(InventoryItem item)
    {
        currentItem = item;
        icon.sprite = item.itemData.icon;
        icon.enabled = true;
        quantityText.text = item.itemData.isStackable && item.quantity > 1
            ? item.quantity.ToString() : "";
    }

    public void Clear()
    {
        icon.sprite = null;
        icon.enabled = false;
        quantityText.text = "";
    }
}
