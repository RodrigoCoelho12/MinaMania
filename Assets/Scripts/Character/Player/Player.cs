using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Player : Character
{
    void Start()
    {
        cc = GetComponent<CharacterController>();

    }

    void Update()
    {
        Move();
        RotatePlayer();
        Dash();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Handle enemy collision
            Debug.Log("Collided with enemy");
        }
    }

    public override void OnTriggerStay(Collider other)
    {

    }

    public override void DeathRoutine()
    {
        throw new System.NotImplementedException();
    }
}
