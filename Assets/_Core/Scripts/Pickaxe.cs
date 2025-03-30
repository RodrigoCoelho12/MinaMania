using Unity.VisualScripting;
using UnityEngine;

public class Pickaxe : Weapon 
{

    public override void Attack()
    {
        GameObject pickaxeModel = transform.GetChild(0).gameObject;
        Collider pickaxeCollider  = GetComponent<Collider>();  
        Animator pickaxeAnimator = pickaxeModel.GetComponent<Animator>();

        if (Input.GetMouseButton(0) && Input.GetMouseButton(1) == false)
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
