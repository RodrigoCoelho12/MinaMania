using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public partial class Player : Character
{
    float yPosition;
    public Image healthBarUI;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        yPosition = transform.position.y;
        
        if (pointsText == null)
            Debug.LogError("pointsText nao atribuido no PlayerRanking.");

        UpdateInterface();
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
        if (other.CompareTag("EnemyAttack"))
        {
            healthBar.AdjustStatusBarBySubtraction(other.GetComponentInParent<Enemy>().damage);
            healthBarUI.fillAmount = healthBar.CurrentBarValue/100;
            Debug.Log("aaa");
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
