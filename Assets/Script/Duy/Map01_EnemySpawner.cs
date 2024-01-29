using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Map01_EnemySpawner : MonoBehaviour
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
        isStartWave = true;
        SpawnEnemies1(3);
        yield return waitFor7Seconds;

        SpawnEnemies1(3);
        yield return waitFor10Seconds;

        SpawnEnemies1(5);
        EndWave();
        yield return waitFor3Seconds;
    }

    private IEnumerator SpawnWave2()
    {
        yield return new WaitUntil(() => isSpawning);
        Debug.Log("Start Wave2");
        currentWave = 2;
        isStartWave = true;
        SpawnEnemies1(5);
        yield return waitFor6Seconds;

        SpawnEnemies1(5);
        yield return waitFor6Seconds;

        SpawnEnemies1(5);
        EndWave();
        yield return waitFor3Seconds;
    }

    private IEnumerator SpawnWave3()
    {
        yield return new WaitUntil(() => isSpawning);
        Debug.Log("Start Wave3");
        currentWave = 3;
        isStartWave = true;
        SpawnEnemies2(2);
        yield return waitFor5Seconds;

        SpawnEnemies1(8);
        yield return waitFor5Seconds;

        SpawnEnemies2(2);
        EndWave();
        yield return waitFor3Seconds;
    }

    private IEnumerator SpawnWave4()
    {
        yield return new WaitUntil(() => isSpawning);
        Debug.Log("Start Wave4");
        currentWave = 4;
        isStartWave = true;
        SpawnEnemies3(3);
        yield return waitFor7Seconds;

        SpawnEnemies3(3);
        EndWave();
        yield return waitFor3Seconds;
    }

    private IEnumerator SpawnWave5()
    {
        yield return new WaitUntil(() => isSpawning);
        Debug.Log("Start Wave5");
        currentWave = 5;
        isStartWave = true;
        SpawnEnemies2(6);
        yield return waitFor6Seconds;

        SpawnEnemies1(8);
        yield return waitFor6Seconds;

        SpawnEnemies1(8);
        EndWave();
        yield return waitFor3Seconds;
    }

    private IEnumerator SpawnWave6()
    {
        yield return new WaitUntil(() => isSpawning);
        Debug.Log("Start Wave6");
        currentWave = 6;
        isStartWave = true;
        SpawnEnemies2(6);
        yield return waitFor6Seconds;

        SpawnEnemies3(3);
        yield return waitFor4Seconds;

        SpawnEnemies1(10);
        yield return waitFor6Seconds;

        SpawnEnemies3(3);
        EndWave();
        yield return waitFor3Seconds;
    }

    private IEnumerator SpawnWave7()
    {
        yield return new WaitUntil(() => isSpawning);
        Debug.Log("Start Wave7");
        currentWave = 7;
        isStartWave = true;
        SpawnEnemies3(5);
        yield return waitFor3Seconds;

        SpawnEnemies3(5);
        yield return waitFor5Seconds;

        SpawnEnemies2(10);
        yield return null;

        SpawnEnemies1(12);
        EndWave();
        yield return waitFor3Seconds;
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
    }
}
