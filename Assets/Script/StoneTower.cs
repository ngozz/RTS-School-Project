using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTower : MonoBehaviour
{
    [SerializeField]
    private float range = 10f;
    [SerializeField]
    private GameObject stonePrefab;
    [SerializeField]
    private GameObject stoneSpawnPoint;
    [SerializeField]
    private float fireRate = 3f;
    [SerializeField]
    private float cooldown = 3f;
    private float timer = 0f;
    private bool canShoot = true;
    private GameObject closestEnemy = null;

    private void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if the timer has reached the fire rate and the tower can shoot
        if (timer >= fireRate && canShoot)
        {
            // Reset the timer
            timer = 0f;

            // Call the method to get the closest enemy with radius of 10
            closestEnemy = GetClosestEnemy(range);

            // If there is an enemy in range, call the Shoot function and pass the enemy position
            if (closestEnemy)
            {
                //Shoot();
                GetComponent<Animator>().SetTrigger("Shoot");

                // Disable shooting until cooldown period is over
                canShoot = false;
                StartCoroutine(EnableShootingAfterCooldown());
            }
        }
    }

    private IEnumerator EnableShootingAfterCooldown()
    {
        // Wait for the cooldown period
        yield return new WaitForSeconds(cooldown);

        // Enable shooting again
        canShoot = true;
    }

    private GameObject GetClosestEnemy(float range)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null && closestDistance <= range)
        {
            Debug.Log("Enemy is in range");
            return closestEnemy;
        }

        return null;
    }

    // Shoot is called from the animation event
    public void Shoot()
    {
        // Create a stone prefab
        GameObject stone = Instantiate(stonePrefab, stoneSpawnPoint.transform.position, Quaternion.identity);

        // Get the enemy's current position
        Vector3 enemyPosition = closestEnemy.transform.position;

        // Get the enemy's direction of movement
        Vector3 enemyDirection = closestEnemy.GetComponent<Rigidbody2D>().velocity.normalized;

        // Calculate a point ahead of the enemy in the direction of their movement
        // The distance ahead is proportional to the fireRate, plus an additional offset
        // Adjust these values as needed
        float offset = 0.5f; // This is the additional offset
        Vector3 futureEnemyPosition = enemyPosition + enemyDirection * (fireRate + offset);

        // Get the direction of the future enemy position
        Vector3 direction = futureEnemyPosition - transform.position;
        direction = direction.normalized;

        // Calculate the vertical distance to the future enemy position
        float verticalDistance = futureEnemyPosition.y - transform.position.y;

        // Split the fireRate into two halves: upwardTime and downwardTime
        float upwardTime = fireRate / 2;
        float downwardTime = fireRate / 2;

        // Adjust upwardTime and downwardTime based on the vertical distance
        if (verticalDistance > 0)
        {
            // The enemy is above
            upwardTime += verticalDistance / 10; // Adjust this value as needed
            downwardTime -= verticalDistance / 10; // Adjust this value as needed
        }
        else
        {
            // The enemy is below
            upwardTime -= Math.Abs(verticalDistance) / 10; // Adjust this value as needed
            downwardTime += Math.Abs(verticalDistance) / 10; // Adjust this value as needed
        }

        // Calculate the initial upward velocity needed to reach the peak in upwardTime seconds
        // The formula is: v = g * t, where g is the gravity and t is the time
        float initialUpwardVelocity = Physics.gravity.magnitude * upwardTime;

        // Calculate the initial velocity needed to reach the future enemy's position in fireRate seconds
        Vector3 initialVelocity = direction * (Vector3.Distance(transform.position, futureEnemyPosition) / fireRate);

        // Set the y component of the initial velocity to the initial upward velocity
        initialVelocity.y = initialUpwardVelocity;

        // Apply the initial velocity
        Rigidbody2D stoneRb = stone.GetComponent<Rigidbody2D>();
        stoneRb.velocity = initialVelocity;

        StartCoroutine(DestroyStoneAfterSeconds(stone, fireRate));
    }

    private IEnumerator DestroyStoneAfterSeconds(GameObject stone, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Animator stoneAnimator = stone.GetComponent<Animator>();
        stoneAnimator.SetTrigger("Break");
    }
}