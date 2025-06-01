using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Player : Character
{
    float yPosition;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        yPosition = transform.position.y;
    }

    void Update()
    {
        Move();
        RotatePlayer();
        Dash();
        MagnetEffect();
        
        pickaxe.Attack();
        waterSpray.Attack();
        dynamite.Attack();

        if (yPosition != transform.position.y)
        {
            this.transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
        }
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
