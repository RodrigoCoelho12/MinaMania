using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;

public partial class Player : Character
{
    float yPosition;
    public Image healthBarUI;
    private GameManager gameManager;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }

        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        yPosition = transform.position.y;

        if (scoreText == null)
            Debug.LogError("scoreText nao atribuido no PlayerRanking.");

        UpdateInterface();

        PlayerSO playerSO = FindAnyObjectByType<PlayerSO>();
        if (playerSO.overrides)
        {
            this.currentWaterSprayData = playerSO.waterSprayData;
            this.currentExtraLifeData = playerSO.extraLifeData;
            this.currentPickaxeData = playerSO.pickaxeData;
            this.currentMagnetData = playerSO.magnetData;
            this.currentDynamiteData = playerSO.dynamiteData;
            this.currentDashData = playerSO.dashData;
            playerSO.overrides = false;
        }
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
            
            if (healthBar.CurrentBarValue <= 0)
            {
                if (gameManager == null)
                {
                    Debug.Log("null");
                    gameManager = GetComponent<GameManager>();
                }
                gameManager.GameOver();
            }
        }

        if (other.CompareTag("Drop"))
        {
            dropCurrency++;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Mineral"))
        {
            //discoveredMinerals.Add(other.gameObject.GetComponent(MineralData));
            Destroy(other.gameObject);
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
