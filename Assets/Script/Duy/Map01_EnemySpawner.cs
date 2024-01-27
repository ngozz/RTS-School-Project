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
    private float timeSinceLastSpawn = 0f;
    private float WaitingTime;
    private float enemiesPerSecond;
    private int enemiesAlive;

    private bool SpawningSooner = false;
    private GameObject currentBtnSpawn;
    private bool isSpawning = false;
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
            StartWave();
            if (currentWave == 0)
            {
                StartGame();
            }
        }

        if (WaitingTime <= 0 && currentWave > 0)
        {
            StartWave();
            currentBtnSpawn.SetActive(false);

        }

        if (isEndLastWave == true && enemiesAlive == 0)
        {
            Debug.Log("WinGame");
            EndGame();
        }

        if (!isSpawning)
        {
            return;
        }
        timeSinceLastSpawn += Time.deltaTime;
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
        if (currentWave < 7)
        {
            isSpawning = false;
            currentBtnSpawn = btnSpawnEnemiesSooner;
            currentBtnSpawn.SetActive(true);
            WaitingTime = 15f;
            timerSpawnSooner.SetDuration(WaitingTime).Begin();
        }
        if (currentWave == 7)
        {
            isSpawning = false;
            isEndLastWave = true;

        }
    }

    void EndGame()
    {
        gameCompleteUI.SetActive(true);

        Time.timeScale = 0f;
    }

    public void SpawnEnemiesSooner()
    {
        SpawningSooner = true;
        currentBtnSpawn.SetActive(false);
    }

    private IEnumerator SpawnWaves()
    {
        yield return StartCoroutine(SpawnWave1());
        yield return StartCoroutine(SpawnWave2());
        yield return StartCoroutine(SpawnWave3());
        yield return StartCoroutine(SpawnWave4());
        yield return StartCoroutine(SpawnWave5());
        yield return StartCoroutine(SpawnWave6());
        yield return StartCoroutine(SpawnWave7());

        // All waves have been spawned
        Debug.Log("All waves completed!");
    }

    private IEnumerator SpawnWave1()
    {
        currentWave = 1;
        SpawnGoblins(3);
        timeSinceLastSpawn = 0f;
        yield return waitFor7Seconds;

        SpawnGoblins(3);
        timeSinceLastSpawn = 0f;
        yield return waitFor10Seconds;

        SpawnGoblins(5);
        timeSinceLastSpawn = 0f;
        EndWave();
    }

    private IEnumerator SpawnWave2()
    {
        if (isSpawning)
        {
            currentWave = 2;
            SpawnGoblins(5);
            timeSinceLastSpawn = 0f;
            yield return waitFor6Seconds;

            SpawnGoblins(5);
            timeSinceLastSpawn = 0f;
            yield return waitFor6Seconds;

            SpawnGoblins(5);
            timeSinceLastSpawn = 0f;
            EndWave();
        }
        else
        {
            yield return new WaitUntil(() => isSpawning);
        }
    }

    private IEnumerator SpawnWave3()
    {
        if (isSpawning)
        {
            currentWave = 3;
            SpawnOrcs(2);
            timeSinceLastSpawn = 0f;
            yield return waitFor5Seconds;

            SpawnGoblins(8);
            timeSinceLastSpawn = 0f;
            yield return waitFor5Seconds;

            SpawnOrcs(2);
            timeSinceLastSpawn = 0f;
            EndWave();
        }
        else
        {
            yield return new WaitUntil(() => isSpawning);
        }
    }

    private IEnumerator SpawnWave4()
    {
        if (isSpawning)
        {
            currentWave = 4;
            SpawnWolfs(3);
            timeSinceLastSpawn = 0f;
            yield return waitFor7Seconds;

            SpawnWolfs(3);
            timeSinceLastSpawn = 0f;
            EndWave();
        }
        else
        {
            yield return new WaitUntil(() => isSpawning);
        }
    }

    private IEnumerator SpawnWave5()
    {
        if (isSpawning)
        {
            currentWave = 5;
            SpawnOrcs(6);
            timeSinceLastSpawn = 0f;
            yield return waitFor6Seconds;

            SpawnGoblins(8);
            timeSinceLastSpawn = 0f;
            yield return waitFor6Seconds;

            SpawnGoblins(8);
            timeSinceLastSpawn = 0f;
            EndWave();
        }
        else
        {
            yield return new WaitUntil(() => isSpawning);
        }
    }

    private IEnumerator SpawnWave6()
    {
        if (isSpawning)
        {
            currentWave = 6;
            SpawnOrcs(6);
            timeSinceLastSpawn = 0f;
            yield return waitFor6Seconds;

            SpawnWolfs(3);
            timeSinceLastSpawn = 0f;
            yield return waitFor4Seconds;

            SpawnGoblins(10);
            timeSinceLastSpawn = 0f;
            yield return waitFor6Seconds;

            SpawnWolfs(3);
            timeSinceLastSpawn = 0f;
            EndWave();
        }
        else
        {
            yield return new WaitUntil(() => isSpawning);
        }
    }

    private IEnumerator SpawnWave7()
    {
        if (isSpawning)
        {
            currentWave = 7;
            SpawnWolfs(5);
            timeSinceLastSpawn = 0f;
            yield return waitFor3Seconds;

            SpawnWolfs(5);
            timeSinceLastSpawn = 0f;
            yield return waitFor5Seconds;

            SpawnOrcs(10);
            timeSinceLastSpawn = 0f;
            yield return null;

            SpawnGoblins(12);
            timeSinceLastSpawn = 0f;
            EndWave();
        }
        else
        {
            yield return new WaitUntil(() => isSpawning);
        }
    }

    private void SpawnGoblins(int count)
    {
        enemiesPerSecond = (count >= 20 || count < 10) ? 0.8f : 1f;
        for (int i = 0; i < count; i++)
        {
            StartCoroutine(WaitFor(i * (1f / enemiesPerSecond), 0));
        }
    }

    private void SpawnOrcs(int count)
    {
        enemiesPerSecond = (count >= 20 || count < 10) ? 0.5f : 0.7f;
        for (int i = 0; i < count; i++)
        {
            StartCoroutine(WaitFor(i * (1f / enemiesPerSecond), 1));
        }
    }

    private void SpawnWolfs(int count)
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
