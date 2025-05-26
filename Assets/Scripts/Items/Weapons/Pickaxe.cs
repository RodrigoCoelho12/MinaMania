using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Pickaxe : Weapon 
{
    private GameObject pickaxeModel;
    private Animator pickaxeAnimator;

    public bool isAttacking = false;

    private void Start()
    {
        pickaxeModel = transform.GetChild(0).gameObject;
        pickaxeAnimator = pickaxeModel.GetComponent<Animator>();
    }

    public override void Attack()
    {
        if (UserInputManager.instance.PickaxeInput && !UserInputManager.instance.SprayInput && !isAttacking)
        {
            Debug.Log("aaaaa");
            pickaxeModel.SetActive(true);
            pickaxeAnimator.SetTrigger("isAttacking");
            
            if (pickaxeAnimator.GetFloat("PickaxeRotation") == 0f)
            {
                pickaxeAnimator.SetFloat("PickaxeRotation", 1f);
            }
            else
            {
                pickaxeAnimator.SetFloat("PickaxeRotation", 0f);
            }
            
            isAttacking = true;
        }

        if (pickaxeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Empty") && isAttacking && !pickaxeAnimator.IsInTransition(0))
        {
            Debug.Log("bbbbbbb");
            isAttacking = false;
            pickaxeModel.SetActive(false);
        }
    }
}
