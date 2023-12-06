using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField]  private GameObject[] enemyPrefabs; 
    
    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
<<<<<<< HEAD:Assets/EnemySpawner.cs
    // Start is called before the first frame update
    void Start()
    {   
        StartWave();
=======

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        StartCoroutine(StartWave());
>>>>>>> 1cdbab6011b4aecaa52c87caf761855cc9b01b01:Assets/Script/EnemySpawner.cs
    }

    private void Update()
    {
        if (!isSpawning) { return; }
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0) {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
<<<<<<< HEAD:Assets/EnemySpawner.cs
        }   
    }
    private void StartWave(){
=======
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }

    }

    private void EnemyDestroyed()
    {
        Debug.Log("Destroy");
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
>>>>>>> 1cdbab6011b4aecaa52c87caf761855cc9b01b01:Assets/Script/EnemySpawner.cs
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

<<<<<<< HEAD:Assets/EnemySpawner.cs
    private void SpawnEnemy(){
        //Debug.Log("Spawn Enemy");
        GameObject prefabToSpawn = enemyPrefabs[0];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position,Quaternion.identity);
    }
    private int EnemiesPerWave(){
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave , difficultyScalingFactor));
=======
    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy()
    {
        GameObject prefabsToSpawn = enemyPrefabs[0];
        Instantiate(prefabsToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
>>>>>>> 1cdbab6011b4aecaa52c87caf761855cc9b01b01:Assets/Script/EnemySpawner.cs
    }
}
