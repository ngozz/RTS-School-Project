using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private int attackDamage = 2;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int livesTaken;
    
    public KnightStats ks;
    private Transform target;
    private int pathIndex = 0;

    public Animator animator;
    public float attackInterval;
    Coroutine attackOrder;
    KnightStats Knight ;

    // Start is called before the first frame update
    void Start()
    {
        target = LevelManager.main.path[0];
    }

    // Update is called once per frame
    void Update()
    {
        //if (detectedKnight /* enemy do not attack knight */) 
        //{
        //    Move();
        //}
        Move();
    }

    IEnumerator Attack()
    {
        animator.Play("Attack");
        //Wait interval
        yield return new WaitForSeconds(attackInterval);
        //Attack again
        attackOrder = StartCoroutine(Attack());
    }

    void Move()
    {
        //animator.Play("New Animation");
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex >= LevelManager.main.path.Length)
            {
                LevelManager.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                LevelManager.main.LifeLeft -= livesTaken;
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    public void InflictDamage()
    {
        Knight.TakeDamage(attackDamage);

        if (ks.currentHealth < 0) 
        {
            //detectedKnight = null;
            StopCoroutine(attackOrder);
            //animator.Play("New Animation");
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (detectedKnight)
        //return;

        if(collision.tag == "Knight")
        {
            Knight = collision.GetComponent<KnightStats>();
            attackOrder =  StartCoroutine(Attack());
        }
    }
}
