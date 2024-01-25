using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightStats : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 100;
    public float attackDamage = 10;
    public int level = 1;
    public int maxLevel = 5;
    public float respawnTime = 10f;
    public float healthRegenRate = 5f;
    public float healthRegenDelay = 5f;
    public float currentXP = 0f;
    public float xpToNextLevel = 100f;
    public float levelUpMultiplier = 1.5f;

    private Animator animator;
    private bool isDead = false;
    private float healthRegenDelayTimer = 0f;

    private float lastRegenTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found.");
        }
    }

    void Update()
    {
        if (!isDead)
        {
            RegenHealth();
        }

        // Debug functions
        if (Input.GetKeyDown(KeyCode.T)) // Press T to take damage
        {
            TakeDamage(50);
            Debug.Log("Current health: " + currentHealth);
        }
        if (Input.GetKeyDown(KeyCode.G)) // Press G to gain XP
        {
            GiveXP(50);
            Debug.Log("Current XP: " + currentXP);
        }
    }

    private void RegenHealth()
    {
        if (currentHealth < maxHealth)
        {
            if (healthRegenDelayTimer < healthRegenDelay)
            {
                healthRegenDelayTimer += Time.deltaTime;
            }
            else
            {
                if (Time.time - lastRegenTime > healthRegenRate)
                {
                    currentHealth += 1;
                    lastRegenTime = Time.time;
                }
            }
        }
    }

    public bool TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthRegenDelayTimer = 0f;
        if (currentHealth <= 0)
        {
            isDead = true;
            animator.SetTrigger("Die");
            StartCoroutine(Respawn());
            return true;
        }
        return false;
    }

    public void GiveXP(float xp)
    {
        currentXP += xp;
        if (currentXP >= xpToNextLevel && level < maxLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level += 1;
        currentXP = 0f;
        xpToNextLevel *= levelUpMultiplier;
        maxHealth *= levelUpMultiplier;
        currentHealth = maxHealth;
        attackDamage *= levelUpMultiplier;
        healthRegenRate *= levelUpMultiplier;
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        isDead = false;
        animator.SetTrigger("Respawn");
        currentHealth = maxHealth / 2;
        healthRegenDelayTimer = 0f;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void Attack()
    {
        GiveXP(10);
    }
}
