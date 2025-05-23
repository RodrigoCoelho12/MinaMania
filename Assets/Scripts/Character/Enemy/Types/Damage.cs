using System.Collections;
using UnityEngine;

public class Damage : Enemy
{
    private void Awake()
    {
        speedValue = 3;
        damageValue = 6;
        //healthController.maxHealth = 35;
    }
    private void Start()
    {
        Attack();
    }
    public override void Attack()
    {
        StartCoroutine(DamageAttack(attackHitbox));
    }

    IEnumerator DamageAttack(GameObject attackHitbox)
    {
        while (true)
        {
            attackHitbox.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            attackHitbox.SetActive(false);

            yield return new WaitForSeconds(4f);
        }
    }
}
