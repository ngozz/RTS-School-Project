using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;

        // Calculate the angle towards the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f - 45f;
        //Debug.Log("Angle: " + angle);

        // Create a quaternion (rotation) based on this angle
        Quaternion bulletRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        // Set the bullet's rotation to this rotation
        transform.rotation = bulletRotation;

        rb.velocity = direction * bulletSpeed;
    }


    void Update()
    {
        // Kiểm tra nếu đạn thoát khỏi màn hình
        if (IsOutOfScreen())
        {
            // Hủy đạn khi thoát khỏi màn hình
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D other) //check collision
    {
        //Take Health from enemy
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        Destroy(gameObject);
    }

    bool IsOutOfScreen()
    {
        // Lấy vị trí của đạn trong không gian màn hình
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        // Kiểm tra nếu đạn thoát khỏi màn hình
        return screenPosition.x < 0 || screenPosition.x > Screen.width ||
               screenPosition.y < 0 || screenPosition.y > Screen.height;
    }
}
