using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/Item/Weapon/WeaponData")]
public class WeaponData : ItemData
{
    [Header("Weapon Attributes")]
    public float damage;
    public virtual string GetTooltip(WeaponData previousData)
    {
        return $"Essa é a {itemName}, ela é {shopDescription}\n, ela causa {(damage - previousData.damage).ToString("+0;-0;0")} de dano.";
    }
}
