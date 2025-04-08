using UnityEngine;

public abstract class Weapon : Item
{
    public int atkValue {  get; private set; }
    public int aoeValue {  get; private set; }


    public void PrintAttackValue()
    {
        Debug.Log("Attack: "+atkValue);
    }
    public void PrintAreaOfEffectValue()
    {
        Debug.Log("AOE diameter (m): " + aoeValue);
    }


    public abstract void Attack();
}
