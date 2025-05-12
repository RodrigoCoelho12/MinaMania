using System.Collections.Generic;
using UnityEngine;

public class Rounds : MonoBehaviour
{
    public GameObject tankPrefab;
    public GameObject speedPrefab;
    public GameObject damagePrefab;
    GameObject player;

    List<Enemy> enemyList = new List<Enemy>(); 
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
            Vector3 instantiatePos = new Vector3(Random.Range(-20f, 20f), 0.6f, Random.Range(-20f, 20f));

            // Escolher um prefab aleatório
            GameObject selectedPrefab = GetRandomPrefab(random);

            // Instanciar e configurar o inimigo
            Enemy enemyScript = Instantiate(selectedPrefab, instantiatePos, Quaternion.identity).GetComponent<Enemy>();
            enemyScript.SetTarget(player);

            // Adicionar à lista se ainda não estiver presente
            if (!enemyList.Contains(enemyScript))
            {
                enemyList.Add(enemyScript);
            }
        }

        round++;
    }

    GameObject GetRandomPrefab(System.Random random)
    {
        int choice = random.Next(3); // Gera um número entre 0 e 2
        return choice switch
        {
            0 => tankPrefab,
            1 => speedPrefab,
            _ => damagePrefab,
        };
    }

    void EndRound(){
        foreach(Enemy enemy in enemyList){           
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
