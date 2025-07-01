using UnityEngine;

[CreateAssetMenu(fileName = "PickaxeData", menuName = "Scriptable Objects/Item/Weapon/PickaxeData")]
public class PickaxeData : WeaponData
{
    [Header("Pickaxe Info Properties")]
    public GameObject pickaxePrefab;
    public int pickaxeIndex;

    public override string GetTooltip(WeaponData previousData)
    {
        if (previousData != null && (previousData is PickaxeData previousDataAsWaterSpray))
        {
            return base.GetTooltip(previousData) + $" ";
        }
        else
        {
            Debug.LogError("Previous data is not of type PickaxeData.");
            return null;
        }
    }
}