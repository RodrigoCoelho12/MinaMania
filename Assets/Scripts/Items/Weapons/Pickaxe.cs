using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Pickaxe : Weapon 
{
    private Animator animator;
    private void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public override void Attack()
    {
        GameObject pickaxeModel = transform.GetChild(0).gameObject;
        Collider pickaxeCollider  = transform.GetChild(0).GetComponent<Collider>();  

        if (UserInputManager.instance.PickaxeInput && !UserInputManager.instance.SprayInput && animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer")).length > animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer")).normalizedTime)
        {
            if(animator.GetFloat("PickaxeRotation") == 0f)
            {
                animator.SetFloat("PickaxeRotation", 1f);
            }else
            {
                animator.SetFloat("PickaxeRotation", 0f);
            }
            pickaxeModel.SetActive(true);
            pickaxeCollider.enabled = true;

        }
        else
        {
            pickaxeModel.SetActive(false);
            pickaxeCollider.enabled = false;
        }
    }
}
