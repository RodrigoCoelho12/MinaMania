using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Pickaxe : Weapon
{
    public override void Attack()
    {
        if (UserInputManager.instance.PickaxeInput)
        {
            //Play Animation
        }
    }
}
