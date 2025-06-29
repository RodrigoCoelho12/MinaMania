using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Pickaxe : Weapon
{
    private Animator animatorPlayer;
    private Animator animatorAttack;
    private Collider pickaxeCollider;
    private ParticleSystem pickaxeParticleSystem;


    public int attackIndex = 1;

    private void Start()
    {
        animatorPlayer = GetComponentInParent<Animator>();
        animatorAttack = GetComponentInChildren<Animator>();
        pickaxeParticleSystem = GetComponentInChildren<ParticleSystem>();
        pickaxeCollider = GetComponentInChildren<Collider>();
    }

    public override void Attack()
    {
        if (UserInputManager.instance.PickaxeInput && !UserInputManager.instance.SprayInput && animatorPlayer.GetBool("IsUsingPickaxe") == false)
        {
            AudioManager.instance.PlaySFX(1);
            
            animatorAttack.SetTrigger("AttackTrigger");
            animatorPlayer.SetBool("IsUsingPickaxe", true);
            gameObject.GetComponentInChildren<Collider>().enabled = true;
            pickaxeParticleSystem.Play();
            pickaxeCollider.enabled = true;

        }
        if(animatorPlayer.GetBool("IsUsingPickaxe") == false)
        {
            pickaxeCollider.enabled = false;
        }
    }
}
