using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public virtual void Attack(){}
    public virtual void WeaponToString()
    {
        //string weaponName = weaponData.itemName;
        //Debug.Log($"Weapon: {weaponName}, Damage: {weaponData.damage}");
    }
}
