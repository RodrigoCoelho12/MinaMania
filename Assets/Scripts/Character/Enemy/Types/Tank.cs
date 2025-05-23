using System.Collections;
using UnityEngine;

public class Tank : Enemy
{
    private void Awake()
    {
        speedValue = 1;
        damageValue = 4;
        //healthController.maxHealth = 50;
    }

    private void Start()
    {
        Attack();
    }
    public override void Attack()
    {
        StartCoroutine(TankAttack(attackHitbox));
    }

    IEnumerator TankAttack(GameObject attackHitbox)
    {
        while (true)
        {
            attackHitbox.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            attackHitbox.SetActive(false);

            yield return new WaitForSeconds(7f);
        }
    }
}
