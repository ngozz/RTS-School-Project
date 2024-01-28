using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StoneTower : MonoBehaviour
{
    [SerializeField] private int attackDamage = 3;
    [SerializeField]
    private float range = 10f;
    [SerializeField]
    private GameObject stonePrefab;
    [SerializeField]
    private GameObject stoneSpawnPoint;
    [SerializeField]
    private float stoneHangTime = 1f;
    [SerializeField]
    private float cooldown = 3f;
    private float timer = 0f;
    private bool canShoot = true;
    private GameObject closestEnemy = null;
    [SerializeField] private GameObject towerSelectionPrefab;
    private GameObject towerSelectionInstance;
    private bool isClicked = false;

    void OnMouseDown()
    {
        if (!isClicked)
        {
            isClicked = true;
            towerSelectionInstance = Instantiate(towerSelectionPrefab, transform.position, Quaternion.identity, transform);
        }
    }
    private void Update()
    {
        Debug.Log("isClicked: " + isClicked + " towerSelectionInstance: " + towerSelectionInstance);
        if (isClicked)
        {
            StartCoroutine(CheckForClicksOutside());
        }
        // Increment the timer
        timer += Time.deltaTime;

        // Check if the timer has reached the fire rate and the tower can shoot
        if (timer >= stoneHangTime && canShoot)
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
        float offset = 0f; // This is the additional offset
        Vector3 futureEnemyPosition = enemyPosition + enemyDirection * (stoneHangTime + offset);

        // Get the direction of the future enemy position
        Vector3 direction = futureEnemyPosition - transform.position;
        direction = direction.normalized;

        // Calculate the vertical distance to the future enemy position
        float verticalDistance = futureEnemyPosition.y - transform.position.y;

        // Split the fireRate into two halves: upwardTime and downwardTime
        float upwardTime = stoneHangTime / 2;
        float downwardTime = stoneHangTime / 2;

        // Adjust upwardTime and downwardTime based on the vertical distance
        if (verticalDistance > 0)
        {
            // The enemy is above
            upwardTime += verticalDistance / 10;
            downwardTime -= verticalDistance / 10;
        }
        else
        {
            // The enemy is below
            upwardTime -= Math.Abs(verticalDistance) / 10;
            downwardTime += Math.Abs(verticalDistance) / 10;
        }

        // Calculate the initial upward velocity needed to reach the peak in upwardTime seconds
        // The formula is: v = g * t, where g is the gravity and t is the time
        float initialUpwardVelocity = Physics.gravity.magnitude * upwardTime;

        // Calculate the initial velocity needed to reach the future enemy's position in fireRate seconds
        Vector3 initialVelocity = direction * (Vector3.Distance(transform.position, futureEnemyPosition) / stoneHangTime);

        // Set the y component of the initial velocity to the initial upward velocity
        initialVelocity.y = initialUpwardVelocity;

        // Apply the initial velocity
        Rigidbody2D stoneRb = stone.GetComponent<Rigidbody2D>();
        stoneRb.velocity = initialVelocity;

        StartCoroutine(DestroyStoneAfterSeconds(stone, stoneHangTime));
    }

    private IEnumerator DestroyStoneAfterSeconds(GameObject stone, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //get all enemies (gameobject with tag enemy) in a radius of 1
        Collider2D[] enemies = Physics2D.OverlapCircleAll(stone.transform.position, 0.5f);
        //loop through all enemies
        foreach (Collider2D enemy in enemies)
        {
            //if the enemy is an enemy
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                //take damage
                enemy.gameObject.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }

        Animator stoneAnimator = stone.GetComponent<Animator>();
        stoneAnimator.SetTrigger("Break");
    }

    IEnumerator CheckForClicksOutside()
    {
        // Wait for a short moment before checking for the click
        yield return new WaitForSeconds(0.1f);

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bool isClickedOnOption = false;
            foreach (Transform option in towerSelectionInstance.transform)
            {
                if (option.GetComponent<Collider2D>().OverlapPoint(mousePos))
                {
                    isClickedOnOption = true;
                    break;
                }
            }
            if (!isClickedOnOption)
            {
                Destroy(towerSelectionInstance);
                isClicked = false;
                Debug.Log("Destroy");
            }
        }
    }

}