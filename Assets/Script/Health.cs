using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int health,maxHealth = 10;
    [SerializeField] private int currencyWorth = 50;
    [SerializeField] FloatingHB healthBar;

    private bool isDestroyed = false;
    private void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
    }
    private void Awake()
    {
        healthBar =GetComponentInChildren<FloatingHB>();
    }
    public void TakeDamage(int dmg)
    {
        health -= dmg;
        healthBar.UpdateHealthBar(health,maxHealth);

        if (health <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
