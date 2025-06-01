using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Pickaxe : Weapon
{
    private GameObject pickaxeModel;
    private Animator pickaxeAnimator;

    public int attackIndex = 1;

    private void Start()
    {
        pickaxeModel = transform.GetChild(0).gameObject;
        pickaxeAnimator = pickaxeModel.GetComponent<Animator>();
    }

    public override void Attack()
    {
        if (UserInputManager.instance.PickaxeInput && !UserInputManager.instance.SprayInput)
        {
            pickaxeModel.SetActive(true);
            pickaxeAnimator.SetInteger("AttackIndex", attackIndex);
        }else if (UserInputManager.instance.SprayInput)
        {
            pickaxeModel.SetActive(false);
        }
    }
}
