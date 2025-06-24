using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Pickaxe : Weapon
{
    private Animator animatorPlayer;
    private Animator animatorAttack;


    public int attackIndex = 1;

    private void Start()
    {
        animatorPlayer = GetComponentInParent<Animator>();
        animatorAttack = GetComponentInChildren<Animator>();
    }

    public override void Attack()
    {
        if (UserInputManager.instance.PickaxeInput && !UserInputManager.instance.SprayInput)
        {
            AudioManager.instance.SwitchSFX(1);
            
            animatorPlayer.SetBool("UsedPickaxe", true);
            animatorAttack.SetTrigger("IsAttacking");
            gameObject.GetComponentInChildren<Collider>().enabled = true;

        }
    }
}
