using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Damage : Enemy
{
    private void Start()
    {
        // Initialize the target to the player
        target = GameObject.Find("Player").transform;
        _hitBox = attackHitbox.GetComponent<HitBox>();
    }

    private void Update()
    {
        Attack();
        Move();
    }

    protected override IEnumerator ShowHitBox()
    {
        while (isAwakened)
        {
            WaitForSeconds hitboxCooldownTime = new WaitForSeconds(attackCooldown);

            animator.SetBool("IsAttacking", true);
            attackHitbox.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            attackHitbox.SetActive(false);
            yield return new WaitForSeconds(0.47f);
            attackHitbox.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            attackHitbox.SetActive(false);
            animator.SetBool("IsAttacking", false);
            yield return hitboxCooldownTime;
        }
    }
}
