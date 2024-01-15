using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class KnightMovement : MonoBehaviour
{
    private bool isSelected = false;
    private bool isManualMove = false;
    private Animator animator;
    private NavMeshAgent agent;
    public float detectionRadius = 3f;
    private float attackAnimationDuration;
    private bool isResetting = false;

    KnightStats knightStats;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        knightStats = GetComponent<KnightStats>();

        attackAnimationDuration = animator.runtimeAnimatorController.animationClips
            .First(clip => clip.name == "Attack").length;
    }

    void Update()
    {
        if (knightStats.IsDead())
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", false);
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(mousePos))
            {
                Debug.Log("Clicked on " + gameObject.name);
                isSelected = true;
            }
            else if (isSelected)
            {
                agent.SetDestination(mousePos);
                isManualMove = true;
            }
        }
        Debug.Log("isManualMove: " + isManualMove);

        if (agent.velocity.magnitude > 0)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Attack", false);
            // Flip the sprite on the X axis based on the direction it's moving
            if (agent.velocity.x > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (agent.velocity.x < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            animator.SetBool("Walk", false);
            GameObject closestEnemy = FindClosestEnemy();
            if (closestEnemy != null && !isManualMove)
            {
                agent.SetDestination(closestEnemy.transform.position);
                animator.SetBool("Attack", true);

                //Change the direction of the sprite based on the direction of the enemy
                if (closestEnemy.transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
            }
            if (!isResetting)
            {
                StartCoroutine(ResetIsManualMoveAfterDelay());
            }
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(enemy.transform.position, currentPos);
            if (dist < minDist && dist <= detectionRadius)
            {
                closest = enemy;
                minDist = dist;
            }
        }
        return closest;
    }

    IEnumerator ResetIsManualMoveAfterDelay()
    {
        isResetting = true;
        yield return new WaitForSeconds(attackAnimationDuration);
        isManualMove = false;
        isResetting = false;
    }
}
