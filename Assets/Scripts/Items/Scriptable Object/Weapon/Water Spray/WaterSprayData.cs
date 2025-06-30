using UnityEngine;

[CreateAssetMenu(fileName = "WaterSprayData", menuName = "Scriptable Objects/Item/Weapon/WaterSprayData")]
public class WaterSprayData : WeaponData
{
    [Header("WaterSpray Attributes")]
    public int sprayRangeIndex;
    public int waterMaxAmount;

    [Header("WaterSpray Object Attributes")]
    public GameObject watersprayPrefab;

    public override string GetTooltip(WeaponData previousData)
    {
        if (previousData != null && (previousData is WaterSprayData previousAsWaterSprayData))
        {
            return base.GetTooltip(previousData) + $" ";
        }
        else
        {
            Debug.LogError("Previous data is not of type WaterSprayData.");
            return null;
        }
    }
}