using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Tank : Enemy
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
        WaitForSeconds animationTime = new WaitForSeconds(attackDuration);
        WaitForSeconds hitboxActiveTime = new WaitForSeconds(0.2f);
        WaitForSeconds hitboxCooldownTime = new WaitForSeconds(attackCooldown);


        while (isAwakened)
        {
            animator.SetBool("IsAttacking", true);
            yield return animationTime;
            attackHitbox.SetActive(true);
            yield return hitboxActiveTime;
            attackHitbox.SetActive(false);
            animator.SetBool("IsAttacking", false);
            yield return hitboxCooldownTime;
        }
    }

    }
