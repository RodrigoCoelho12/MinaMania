using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Pickaxe : Weapon 
{

    public override void Attack()
    {
        GameObject pickaxeModel = transform.GetChild(0).gameObject;
        Collider pickaxeCollider  = GetComponent<Collider>();  
        Animator pickaxeAnimator = pickaxeModel.GetComponent<Animator>();

        if (UserInputManager.instance.PickaxeInput && !UserInputManager.instance.SprayInput)
        {
            pickaxeModel.SetActive(true);
            pickaxeCollider.enabled = true;
            pickaxeAnimator.enabled = true;
        }
        else
        {
            pickaxeModel.SetActive(false);
            pickaxeCollider.enabled = false;
            pickaxeAnimator.enabled = false;
        }
    }
}
