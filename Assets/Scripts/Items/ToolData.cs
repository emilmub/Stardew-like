using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Items/Tool")]

public class ToolData : ItemData
{
    public int power;
    public float staminaCost;

    public enum ToolType { Axe, Hoe, Pickaxe, WateringCan};
    public ToolType toolType;

    public override void Use()
    {
        ToolManager.instance.UseTool(this);
    }
}
