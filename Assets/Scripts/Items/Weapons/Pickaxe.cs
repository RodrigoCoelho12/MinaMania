using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Pickaxe : Weapon
{
    private Animator animator;

    public int attackIndex = 1;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    public override void Attack()
    {
        if (UserInputManager.instance.PickaxeInput && !UserInputManager.instance.SprayInput)
        {
            AudioManager.instance.SwitchSFX(1);
            
            animator.SetTrigger("UsedPickaxe");

        }
        else if (UserInputManager.instance.SprayInput)
        {
            animator.SetTrigger("StartedWaterSpray");
        }
    }
}
