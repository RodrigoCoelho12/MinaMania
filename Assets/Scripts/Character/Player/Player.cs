using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public partial class Player : Character
{
    float yPosition;
    public Image healthBarUI;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        yPosition = transform.position.y;
        
        if (scoreText == null)
            Debug.LogError("pointsText nao atribuido no PlayerRanking.");

        saveSystem = new SaveSystem();
        playerDataList = saveSystem.LoadPlayerDataList();

        string playerName = "Player";

        playerData = playerDataList.players.FirstOrDefault(p => p.name == playerName);

        if (playerData == null)
        {
            playerData = new PlayerData(playerName, 0);
            playerDataList.players.Add(playerData);
        }

        score = playerData.score;

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
        //dynamite.Attack();

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
