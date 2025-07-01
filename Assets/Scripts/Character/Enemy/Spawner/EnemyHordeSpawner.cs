using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class EnemyHordeSpawner : MonoBehaviour
{
    [Header("Enemy Types")]
    public GameObject[] enemyPrefabs;

    [Header("Spawn Area")]
    public Transform[] spawnPoints;

    [Header("Spawn Settings")]
    public float spawnInterval = 1f;
    public float delayAfterLastEnemyDies = 10f;

    [Header("Horde Growth Settings")]
    public int initialEnemyCount = 1;
    public int growthPerHorde = 2;

    [Header("UI")]
    public TextMeshProUGUI countdownHordeText;
    public GameObject goToTheStore;
    public Collider doorCollider;

    private Queue<HordeData> hordeQueue = new Queue<HordeData>();
    public int hordeCount;
    private bool canSpawn = true;

    private List<GameObject> activeEnemies = new List<GameObject>();

    [Header("Drop Types")]
    public GameObject[] dropPrefabs;
    private Queue<GameObject> dropItemQueue = new Queue<GameObject>();

    private void Start()
    {

        hordeCount = PlayerSO.Instance.hordeCount;

        goToTheStore.SetActive(false);

        if( hordeCount > 0)
        {
            hordeQueue.Enqueue(new HordeData(initialEnemyCount * growthPerHorde * hordeCount));
        }
        else
        {
            hordeQueue.Enqueue(new HordeData(initialEnemyCount));
        }

        foreach (GameObject item in dropPrefabs)
        {
            dropItemQueue.Enqueue(item);
        }
        
        for(int i = 0; i < PlayerSO.Instance.discoveredMinerals.Count; i++ )
        {
            var ItemQueue = dropItemQueue.First();
            dropItemQueue.Dequeue();
            dropItemQueue.Enqueue(ItemQueue);
        }


        

        StartCoroutine(HordeLoop());
    }

    IEnumerator HordeLoop()
    {
        while (canSpawn)
        {
            float currentDelay = delayAfterLastEnemyDies;

            yield return StartCoroutine(Countdown(currentDelay));
            
            if (hordeQueue.Count == 0)
            {
                int nextEnemyCount = initialEnemyCount + growthPerHorde * hordeCount;
                hordeQueue.Enqueue(new HordeData(nextEnemyCount));
            }

            HordeData currentHorde = hordeQueue.Dequeue();

            hordeCount++;
            //Debug.Log("Horda: " + hordeCount);
            yield return StartCoroutine(SpawnHorde(currentHorde));
            yield return new WaitUntil(() => AllEnemiesDead());

            DropItem();
            StopSpawning();
            goToTheStore.SetActive(true);
            doorCollider.enabled = true;
        }
    }

    public void DropItem()
    {
        var item = dropItemQueue.First();
        
        Instantiate(item, Vector3.zero, Quaternion.identity);

        dropItemQueue.Dequeue();
        dropItemQueue.Enqueue(item);
    }

    IEnumerator SpawnHorde(HordeData horde)
    {
        activeEnemies.Clear();

        for (int i = 0; i < horde.enemyCount; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject chosenEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            GameObject enemy = Instantiate(chosenEnemy, spawnPoint.position, Quaternion.identity);
            activeEnemies.Add(enemy);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    bool AllEnemiesDead()
    {
        activeEnemies.RemoveAll(enemy => enemy == null || enemy.activeSelf == false);
        //Debug.Log("Inimigos restantes: " + activeEnemies.Count);

        return activeEnemies.Count == 0;
    }

    public void StopSpawning()
    {
        canSpawn = false;
    }

    IEnumerator Countdown(float time)
    {
        if (countdownHordeText != null)
            countdownHordeText.gameObject.SetActive(true);

        while (time > 0)
        {
            if (countdownHordeText != null)
                countdownHordeText.text = "Proxima horda em: " + Mathf.Ceil(time).ToString();

            time -= Time.deltaTime;
            yield return null;
        }

        if (countdownHordeText != null)
        {
            countdownHordeText.text = "";
            countdownHordeText.gameObject.SetActive(false);
        }
    }
}
