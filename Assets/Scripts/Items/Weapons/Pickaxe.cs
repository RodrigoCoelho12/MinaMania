using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Pickaxe : Weapon
{
    private Animator animatorPlayer;
    private Animator animatorAttack;
    private ParticleSystem pickaxeParticleSystem;


    public int attackIndex = 1;

    private void Start()
    {
        animatorPlayer = GetComponentInParent<Animator>();
        animatorAttack = GetComponentInChildren<Animator>();
        pickaxeParticleSystem = GetComponentInChildren<ParticleSystem>(); 
    }

    public override void Attack()
    {
        if (UserInputManager.instance.PickaxeInput && !UserInputManager.instance.SprayInput && animatorPlayer.GetBool("IsUsingPickaxe") == false)
        {
           // AudioManager.instance.PlaySFX(1);
            
            animatorAttack.SetTrigger("AttackTrigger");
            gameObject.GetComponentInChildren<Collider>().enabled = true;
            pickaxeParticleSystem.Play();

            animatorPlayer.SetBool("IsUsingPickaxe", true);

        }
    }
}
