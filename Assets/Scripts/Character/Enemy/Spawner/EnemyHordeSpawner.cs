using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHordeSpawner : MonoBehaviour
{
    [Header("Player Reference")]
    private GameObject player;

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

    private Queue<HordeData> hordeQueue = new Queue<HordeData>();
    private int hordeCount = 0;
    private bool canSpawn = true;

    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Start()
    {
        player = GameObject.Find("Player");

        hordeQueue.Enqueue(new HordeData(initialEnemyCount));
        hordeQueue.Enqueue(new HordeData(initialEnemyCount + growthPerHorde));
        hordeQueue.Enqueue(new HordeData(initialEnemyCount + growthPerHorde * 2));

        StartCoroutine(HordeLoop());
    }

    IEnumerator HordeLoop()
    {
        while (canSpawn)
        {
            if (hordeQueue.Count == 0)
            {
                int nextEnemyCount = initialEnemyCount + growthPerHorde * hordeCount;
                hordeQueue.Enqueue(new HordeData(nextEnemyCount));
            }

            HordeData currentHorde = hordeQueue.Dequeue();
            hordeCount++;

            yield return StartCoroutine(SpawnHorde(currentHorde));
            yield return new WaitUntil(() => AllEnemiesDead());

            float currentDelay = delayAfterLastEnemyDies;

            yield return StartCoroutine(Countdown(currentDelay));

            int futureEnemyCount = initialEnemyCount + growthPerHorde * hordeCount;
            hordeQueue.Enqueue(new HordeData(futureEnemyCount));
        }
    }

    IEnumerator SpawnHorde(HordeData horde)
    {
        player.GetComponent<Player>().IncreasePoints();
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
        activeEnemies.RemoveAll(enemy => enemy == null);
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
                countdownHordeText.text = "Próxima horda em: " + Mathf.Ceil(time).ToString() + "s";

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