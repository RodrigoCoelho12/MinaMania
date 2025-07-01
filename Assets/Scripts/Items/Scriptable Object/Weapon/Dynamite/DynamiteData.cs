using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "DynamiteData", menuName = "Scriptable Objects/Item/Weapon/DynamiteData")]

public class DynamiteData : WeaponData
{
    [Header("Dynamite Info Properties")]
    public GameObject dynamitePrefab;
    public float explosionRadius;
    public float explosionForce;
    public int dynamiteAmount;
    public int dynamiteIndex;

    public override string GetTooltip(WeaponData previousData)
    {
        if (previousData != null && (previousData is WeaponData previousDynamiteData))
        {
            return base.GetTooltip(previousData) + $" ";
        }
        else
        {
            Debug.LogError("Previous data is not of type DynamiteData.");
            return null;
        }
    }
}
