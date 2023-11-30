using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyMovement : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;


    private void Start()
    {
        // Khởi tạo rb nếu nó chưa được khởi tạo
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();

            // Kiểm tra xem rb đã được khởi tạo thành công hay không
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D is not initialized!");
            }
        }

        target = LevelManager.main.path[pathIndex];
    }



    private void Update()
    {
        // Kiểm tra xem target đã được khởi tạo chưa
        if (target != null && Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }


    private void FixedUpdate()
    {
        // Kiểm tra xem rb đã được khởi tạo chưa
        if (rb != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            Debug.LogError("Rigidbody2D is null in FixedUpdate!");
        }
    }


}