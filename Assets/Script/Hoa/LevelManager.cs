using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Scripting;
using TMPro;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int currency;
    public int LifeLeft = 20;
    public TextMeshProUGUI Life;
    public TextMeshProUGUI Gold;
    public int enemiesAlive = 0;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    public static UnityEvent onEnemySpawn = new UnityEvent();
    public static UnityEvent onEnemyAlive = new UnityEvent();



    private void Awake()
    {
        main = this;
        onEnemyDestroy.AddListener(EnemyDestroyed);
        onEnemySpawn.AddListener(EnemySpawn);
        //onEnemyAlive.AddListener(GetEnemyAlive);
    }

    private void Start()
    {
        currency = 300;
    }

    //sử dụng chung cho các map, đếm số enemy còn lại
    private void EnemyDestroyed()
    {
        Debug.Log("Kill enemy");
        enemiesAlive--;
    }

    private void EnemySpawn()
    {
        Debug.Log("Spawn Enemy");
        enemiesAlive++;
    }

    public int GetEnemyAlive()
    {
        return enemiesAlive;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("You do not have enough to purchase this item");
            return false;
        }
    }

    private void Update()
    {
        Gold.text = currency.ToString();
        Life.text = LifeLeft.ToString();
    }

}
