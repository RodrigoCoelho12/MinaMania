using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    #region Weapon Properties
    [Header("Weapon Properties")]
    public WeaponData weaponData;
    #endregion
    public virtual void Attack(){}
    public virtual void WeaponToString()
    {
        string weaponName = weaponData.itemName;
        Debug.Log($"Weapon: {weaponName}, Damage: {weaponData.damage}");
    }
}
