using System.Collections;
using UnityEngine;

public class Speed : Enemy
{
    private void Awake()
    {
        speedValue = 5;
        damageValue = 2;
        //healthController.maxHealth = 20;
    }
    private void Start()
    {
        Attack();
    }
    public override void Attack()
    {
        StartCoroutine(SpeedAttack(attackHitbox));
    }

    IEnumerator SpeedAttack(GameObject attackHitbox)
    {
        while (true)
        {
            attackHitbox.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            attackHitbox.SetActive(false);
            
            yield return new WaitForSeconds(2f);
        }
    }
}
