using UnityEngine;

public abstract class Weapon : Item
{
    public int atkValue;
    public int atkRadius;


    public void PrintAttackValue()
    {
        Debug.Log("Attack: "+atkValue);
    }
    public void PrintAreaOfEffectValue()
    {
        Debug.Log("Radius of Attack: " + atkRadius);
    }


    public abstract void Attack();
}
