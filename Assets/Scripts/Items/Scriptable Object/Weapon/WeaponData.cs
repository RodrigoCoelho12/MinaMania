using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/Item/Weapon/WeaponData")]
public class WeaponData : ItemData
{
    [Header("Weapon Attributes")]
    public float damage;
    public virtual string GetTooltip(WeaponData previousData)
    {
        return $"{description}";
    }
}
