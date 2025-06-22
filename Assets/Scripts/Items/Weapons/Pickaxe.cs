using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Pickaxe : Weapon
{
    private GameObject pickaxeModel;
    private Animator pickaxeAnimator;

    private int pickaxeAttackCount;

    public int attackIndex = 1;

    private void Start()
    {
        pickaxeModel = transform.GetChild(0).gameObject;
        pickaxeAnimator = pickaxeModel.GetComponent<Animator>();

        pickaxeAttackCount = 0;
    }

    public override void Attack()
    {
        if (UserInputManager.instance.PickaxeInput && !UserInputManager.instance.SprayInput)
        {
            pickaxeModel.SetActive(true);
            AudioManager.instance.SwitchSFX(1);
            pickaxeAnimator.SetInteger("AttackIndex", attackIndex);

            pickaxeAttackCount++;
            AnalyticsTest.Instance.AddAnalytics("Player", "PickAxe Attack", pickaxeAttackCount.ToString());
        }
        else if (UserInputManager.instance.SprayInput)
        {
            pickaxeModel.SetActive(false);
        }
    }
}
