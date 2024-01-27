using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSupport : MonoBehaviour
{
    [SerializeField]
    private float range = 5f;
    public int damage = 2;

    private bool canUse = true;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (canUse)
        {
            // Take health from enemy
            other.gameObject.GetComponent<Health>().TakeDamage(damage);

            // Destroy the object
            Destroy(gameObject);

            // Disable the object temporarily
            StartCoroutine(EnableAfterSeconds(5f));
        }
    }

    private IEnumerator EnableAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canUse = true;
    }

    // Call this method when you want to reuse the object
    public void ReuseObject()
    {
        canUse = true;
    }
}
