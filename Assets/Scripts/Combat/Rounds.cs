using System.Collections.Generic;
using UnityEngine;

public class Rounds : MonoBehaviour
{
    public GameObject tankPrefab;
    public GameObject speedPrefab;
    public GameObject damagePrefab;
    GameObject player;

    List<EnemyProto> enemyList = new List<EnemyProto>(); 
    int round;

    void Start()
    {
        round = 0;
        player = GameObject.Find("Player");
        InitializeRound();
    }

    void InitializeRound()
    {
        int enemyQuantity = round + 10;
        System.Random random = new System.Random();

        for (int i = 0; i < enemyQuantity; i++)
        {
            Vector3 instantiatePosition = new Vector3(Random.Range(-20f, 20f), 0.6f, Random.Range(-20f, 20f));

            GameObject selectedPrefab = GetRandomPrefab(random);

            EnemyProto enemyScript = Instantiate(selectedPrefab, instantiatePosition, Quaternion.identity).GetComponent<EnemyProto>();
            enemyScript.SetTarget(player);

            if (!enemyList.Contains(enemyScript))
            {
                enemyList.Add(enemyScript);
            }
        }

        round++;
    }

    GameObject GetRandomPrefab(System.Random random)
    {
        int choice = random.Next(3); 
        return choice switch
        {
            0 => tankPrefab,
            1 => speedPrefab,
            _ => damagePrefab,
        };
    }

    void EndRound(){
        foreach(EnemyProto enemy in enemyList){           
            enemy.DeathRoutine();
        }
        enemyList.Clear();  
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            InitializeRound();
        }
        if(Input.GetKeyDown(KeyCode.O)){
            EndRound();
        }
    }
}
