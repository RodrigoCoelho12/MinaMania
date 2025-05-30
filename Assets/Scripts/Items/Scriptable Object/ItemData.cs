using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/Item/ItemData")]

public class ItemData : ScriptableObject
{
    [Header("Item Info Properties")]
    public string itemName;
    public string description;
    public string shopDescription;
    public Mesh itemMesh;
    public Material itemMaterial;

    [Header("Item Attributes")]
    public int itemPrice;
    public virtual string GetTooltip(ItemData previousData)
    {
        return "Item";
    }
}