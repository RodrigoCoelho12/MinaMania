using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;

public partial class Player : Character
{
    float yPosition;
    public Image healthBarUI;
    public TextMeshProUGUI mineralCollectedMessage;
    private GameManager gameManager;


    private void Awake()
    {
        currentWaterSprayData = PlayerSO.Instance.waterSprayData;
        currentExtraLifeData = PlayerSO.Instance.extraLifeData;
        currentPickaxeData = PlayerSO.Instance.pickaxeData;
        currentMagnetData = PlayerSO.Instance.magnetData;
        currentDynamiteData = PlayerSO.Instance.dynamiteData;
        currentDashData = PlayerSO.Instance.dashData;
        dropCurrency = PlayerSO.Instance.playerCurrency;
        score = PlayerSO.Instance.score;

        foreach (var mineral in PlayerSO.Instance.discoveredMinerals)
        {
            discoveredMinerals.Add(mineral);
        }
        foreach (var skill in PlayerSO.Instance.discoveredSkills)
        {
            discoveredSkills.Add(skill);
        }
        foreach (var weapon in PlayerSO.Instance.discoveredWeapons)
        {
            discoveredWeapons.Add(weapon);
        }
    }
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
        
        if (hasExtraLife)
        {
            extraLifeIndicator.SetActive(true);
        }

        UpdateCurrencyUI();
        UpdateScoreInterface();

        CheckSkills();
        CheckWeapons();

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
            StartCoroutine(ChangePlayerColor());


            if (healthBar.CurrentBarValue <= 0)
            {
                if (gameManager == null)
                {
                    Debug.Log("null");
                    gameManager = GetComponent<GameManager>();
                }
                
                ApplyExtraLife();
            }
        }

        if (other.CompareTag("Drop"))
        {
            dropCurrency++;
            Destroy(other.gameObject);
            UpdateCurrencyUI();
        }
        
        if (other.CompareTag("Mineral"))
        {
            StartCoroutine(PlayMineralCollectedMessage(other));
        }
    }

    public override void OnTriggerStay(Collider other)
    {

    }

    public void UpdateCurrencyUI()
    {
        currencyText.text = ": " +dropCurrency.ToString();
    }

    private IEnumerator PlayMineralCollectedMessage(Collider other)
    {
        mineralCollectedMessage.text = $"Voce descobriu um/uma {other.gameObject.GetComponent<MineralController>().mineralData.itemName}!";
        yield return new WaitForSeconds(5f);
        mineralCollectedMessage.text = " ";
    }

    private IEnumerator ChangePlayerColor()
    {
        gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
    }

    public void CheckSkills()
    {
        if(currentDashData != null)
        {
            hasDash = true;
        }
        if (currentMagnetData != null)
        {
            hasMagnet = true;
        }
        if (currentExtraLifeData != null)
        {
            hasExtraLife = true;
            extraLifeIndicator.SetActive(true);
        }
    }

    public void CheckWeapons()
    {
        switch(currentPickaxeData.pickaxeIndex)
        {
            case 0:
                PickaxeEvo2.SetActive(false);
                PickaxeEvo1.SetActive(false);
                PickaxeStandard.SetActive(true);
                break;
            case 1:
                PickaxeStandard.SetActive(false);
                PickaxeEvo2.SetActive(false);
                PickaxeEvo1.SetActive(true);
            break;
            
            case 2:
                PickaxeEvo2.SetActive(true);
                PickaxeEvo1.SetActive(false);
                PickaxeStandard.SetActive(false);
             break;
        }
        switch (currentWaterSprayData.waterSprayIndex)
        {
            case 0:
                WaterSprayEvo2.SetActive(false);
                WaterSprayEvo1.SetActive(false);
                WaterSprayStandard.SetActive(true);
                break;
            case 1:
                WaterSprayEvo2.SetActive(false);
                WaterSprayEvo1.SetActive(true);
                WaterSprayStandard.SetActive(false);
                break;

            case 2:
                WaterSprayEvo2.SetActive(true);
                WaterSprayEvo1.SetActive(false);
                WaterSprayStandard.SetActive(false);
                break;
        }
    }

    public override void DeathRoutine()
    {
        gameManager.GameOver();
    }
}
