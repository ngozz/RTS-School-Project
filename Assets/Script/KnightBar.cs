using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightBar : MonoBehaviour
{
    [SerializeField] private KnightStats knightStats;
private Slider healthBar;
private Slider xpBar;

    // Update is called once per frame

    private void Start()
    {
        //get healhbar and xpbar components. They're in the children of the gameobject with this script with name "HP" and "XP"
        healthBar = transform.Find("HP").GetComponent<Slider>();
        xpBar = transform.Find("XP").GetComponent<Slider>();
    }

    void Update()
    {
        healthBar.value = knightStats.currentHealth / knightStats.maxHealth;
        xpBar.value = knightStats.currentXP / knightStats.xpToNextLevel;
    }
}
