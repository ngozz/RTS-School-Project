using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Scripting;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int currency;
    public int LifeLeft = 20;
    public TextMeshProUGUI Life;
    public TextMeshProUGUI Gold;

    private void Awake()
    {
        main = this;
    }

    private void Start() 
    {
        currency = 300;
    }

    public void IncreaseCurrency(int amount) {
        currency += amount;
    }

    public bool SpendCurrency(int amount) {
        if (amount <= currency) {
            currency -= amount;
            return true;
        } else {
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
