using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Map01_EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    private int NumberOfWave = 7;
    private int currentWave = 1;
    private float difficultyScalingFactor = 1f;
    private float WaitingTime = 22f;
    private float TimeSinceLastSpawn;
    private int EnemiesAlive;
    private int EnemiesLeftToSpawn;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private bool isSpawning = false;
    public TextMeshProUGUI Wave;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        Wave.text = "Wave" + " " + currentWave.ToString() + " / 5";
        if (!isSpawning) { return; }

        if(EnemiesLeftToSpawn == 0)
        {
            SpawnEnemy();

        }
    }

    private void EnemyDestroyed()
    {
        EnemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(5);
        isSpawning = true;
        EnemiesLeftToSpawn = EnemiesPerWave();
    }

    private void EndWave()
    {
        isSpawning = false;
        TimeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[randomIndex];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}
