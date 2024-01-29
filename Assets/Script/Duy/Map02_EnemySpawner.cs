using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Map02_EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject btnSpawnEnemiesSoonerFirst;
    [SerializeField] private GameObject btnSpawnEnemiesSooner;
    [SerializeField] Timer timerSpawnSooner;
    [SerializeField] private TextMeshProUGUI Wave;
    [SerializeField] public GameObject gameCompleteUI;

    [Header("Attributes")]
    private int currentWave = 0;
    private float WaitingTime;
    private float enemiesPerSecond;
    private int enemiesAlive;

    private bool SpawningSooner = false;
    private GameObject currentBtnSpawn;
    private bool isSpawning = false;
    private bool isEndWave = false;
    private bool isStartWave = false;
    private bool isEndLastWave = false;
    private LevelManager levelManager;
    private int enemiesLeftToSpawn;

    private WaitForSeconds waitFor7Seconds = new WaitForSeconds(7f);
    private WaitForSeconds waitFor6Seconds = new WaitForSeconds(6f);
    private WaitForSeconds waitFor10Seconds = new WaitForSeconds(10f);
    private WaitForSeconds waitFor5Seconds = new WaitForSeconds(5f);
    private WaitForSeconds waitFor4Seconds = new WaitForSeconds(4f);
    private WaitForSeconds waitFor3Seconds = new WaitForSeconds(3f);

    private void Start()
    {
        currentBtnSpawn = btnSpawnEnemiesSoonerFirst;
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        Wave.text = "Wave" + " " + currentWave.ToString() + " / 7";
        WaitingTime -= Time.deltaTime;
        enemiesAlive = levelManager.GetEnemyAlive();
        if (SpawningSooner)
        {
            isEndWave = false;
            StartWave();
            if (currentWave == 0)
            {
                StartGame();
            }
            if (isStartWave)
            {
                isStartWave = false;
            }
        }

        if (WaitingTime <= 0 && currentWave > 0 && isEndWave)
        {
            isEndWave = false;
            StartWave();
            currentBtnSpawn.SetActive(false);
            if (isStartWave)
            {
                isStartWave = false;
            }
        }

        if (isEndLastWave == true && enemiesAlive == 0)
        {
            //Debug.Log("WinGame");
            EndGame();
        }

        if (!isSpawning)
        {
            return;
        }
    }

    private void StartGame()
    {
        StartCoroutine(SpawnWaves());
    }

    private void StartWave()
    {
        isSpawning = true;
        SpawningSooner = false;
    }

    private void EndWave()
    {
        isSpawning = false;
        isEndWave = true;
        if (currentWave < 7)
        {
            currentBtnSpawn = btnSpawnEnemiesSooner;
            currentBtnSpawn.SetActive(true);
            WaitingTime = 15f;
            timerSpawnSooner.SetDuration(WaitingTime).Begin();
        }
        if (currentWave == 7)
        {
            isEndLastWave = true;
        }
    }

    void EndGame()
    {
        gameCompleteUI.SetActive(true);

    }

    public void SpawnEnemiesSooner()
    {
        SpawningSooner = true;
        currentBtnSpawn.SetActive(false);
    }

    private IEnumerator SpawnWaves()
    {
        Debug.Log("Start Wave1");
        yield return StartCoroutine(SpawnWave1());
        yield return new WaitUntil(() => isEndWave);
        yield return StartCoroutine(SpawnWave2());
        yield return new WaitUntil(() => isEndWave);
        yield return StartCoroutine(SpawnWave3());
        yield return new WaitUntil(() => isEndWave);
        yield return StartCoroutine(SpawnWave4());
        yield return new WaitUntil(() => isEndWave);
        yield return StartCoroutine(SpawnWave5());
        yield return new WaitUntil(() => isEndWave);
        yield return StartCoroutine(SpawnWave6());
        yield return new WaitUntil(() => isEndWave);
        yield return StartCoroutine(SpawnWave7());

        // All waves have been spawned
        Debug.Log("All waves completed!");
    }

    private IEnumerator SpawnWave1()
    {
        currentWave = 1;
        SpawnEnemies1(3);
        yield return waitFor7Seconds;

        SpawnEnemies1(3);
        yield return waitFor10Seconds;

        enemiesLeftToSpawn = 5;
        SpawnEnemies1(5);
        yield return new WaitUntil(() => enemiesLeftToSpawn == 0);
        yield return waitFor3Seconds;
        EndWave();
    }

    private IEnumerator SpawnWave2()
    {
        yield return new WaitUntil(() => isSpawning);
        Debug.Log("Start Wave2");
        currentWave = 2;
        isStartWave = true;
        enemiesLeftToSpawn = 5;
        SpawnEnemies1(5);
        yield return new WaitUntil(() => enemiesLeftToSpawn == 0);
        yield return waitFor6Seconds;

        enemiesLeftToSpawn = 5;
        SpawnEnemies1(5);
        yield return new WaitUntil(() => enemiesLeftToSpawn == 0);
        yield return waitFor6Seconds;

        enemiesLeftToSpawn = 5;
        SpawnEnemies1(5);
        yield return new WaitUntil(() => enemiesLeftToSpawn == 0);
        yield return waitFor3Seconds;
        EndWave();
        yield return waitFor3Seconds;
    }

    private IEnumerator SpawnWave3()
    {
        yield return new WaitUntil(() => isSpawning);
        currentWave = 3;
        SpawnEnemies2(2);
        yield return null;

        SpawnEnemies1(5);
        yield return waitFor5Seconds;

        enemiesLeftToSpawn = 2;
        SpawnEnemies2(2);
        yield return new WaitUntil(() => enemiesLeftToSpawn == 0);
        yield return waitFor3Seconds;
        EndWave();
    }

    private IEnumerator SpawnWave4()
    {
        yield return new WaitUntil(() => isSpawning);
        currentWave = 4;
        SpawnEnemies3(3);
        yield return waitFor7Seconds;

        enemiesLeftToSpawn = 3;
        SpawnEnemies3(3);
        yield return new WaitUntil(() => enemiesLeftToSpawn == 0);
        yield return waitFor3Seconds;
        EndWave();
    }

    private IEnumerator SpawnWave5()
    {
        yield return new WaitUntil(() => isSpawning);
        currentWave = 5;
        SpawnEnemies2(5);
        yield return waitFor6Seconds;

        SpawnEnemies1(6);
        yield return waitFor6Seconds;

        enemiesLeftToSpawn = 5;
        SpawnEnemies1(5);
        yield return new WaitUntil(() => enemiesLeftToSpawn == 0);
        yield return waitFor3Seconds;
        EndWave();
    }

    private IEnumerator SpawnWave6()
    {
        yield return new WaitUntil(() => isSpawning);
        currentWave = 6;
        enemiesLeftToSpawn = 6;
        SpawnEnemies2(6);
        yield return new WaitUntil(() => enemiesLeftToSpawn == 0);
        yield return waitFor3Seconds;

        enemiesLeftToSpawn = 3;
        SpawnEnemies3(3);
        yield return new WaitUntil(() => enemiesLeftToSpawn == 0);
        yield return waitFor4Seconds;

        enemiesLeftToSpawn = 10;
        SpawnEnemies1(10);
        yield return new WaitUntil(() => enemiesLeftToSpawn == 0);
        yield return waitFor6Seconds;

        enemiesLeftToSpawn = 3;
        SpawnEnemies3(3);
        yield return new WaitUntil(() => enemiesLeftToSpawn == 0);
        yield return waitFor3Seconds;
        EndWave();
    }

    private IEnumerator SpawnWave7()
    {
        yield return new WaitUntil(() => isSpawning);
        currentWave = 7;
        SpawnEnemies3(4);
        yield return waitFor3Seconds;

        enemiesLeftToSpawn = 5;
        SpawnEnemies3(4);
        yield return new WaitUntil(() => enemiesLeftToSpawn == 0);
        yield return waitFor5Seconds;

        enemiesLeftToSpawn = 10;
        SpawnEnemies2(10);
        yield return new WaitUntil(() => enemiesLeftToSpawn == 0);
        yield return waitFor3Seconds;

        SpawnEnemies1(10);
        EndWave();
    }

    private void SpawnEnemies1(int count)
    {
        enemiesPerSecond = (count >= 20 || count < 10) ? 0.8f : 1f;
        for (int i = 0; i < count; i++)
        {
            StartCoroutine(WaitFor(i * (1f / enemiesPerSecond), 0));
        }
    }

    private void SpawnEnemies2(int count)
    {
        enemiesPerSecond = (count >= 20 || count < 10) ? 0.5f : 0.7f;
        for (int i = 0; i < count; i++)
        {
            StartCoroutine(WaitFor(i * (1f / enemiesPerSecond), 1));
        }
    }

    private void SpawnEnemies3(int count)
    {
        enemiesPerSecond = 0.25f;
        for (int i = 0; i < count; i++)
        {
            StartCoroutine(WaitFor(i * (1f / enemiesPerSecond), 2));
        }
    }

    private IEnumerator WaitFor(float time, int type)
    {
        yield return new WaitForSeconds(time);
        Instantiate(enemyPrefabs[type], LevelManager.main.startPoint.position, Quaternion.identity);
        LevelManager.onEnemySpawn.Invoke();
        enemiesLeftToSpawn--;
    }
}