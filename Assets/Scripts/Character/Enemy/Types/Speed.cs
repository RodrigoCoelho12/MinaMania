using System.Collections;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Speed : Enemy
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
}
