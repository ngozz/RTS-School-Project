using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSupport : MonoBehaviour
{
    [SerializeField]
    public float range = 5f;
    public int damage = 2;

    private void OnCollisionEnter2D(Collision2D other) //check collision
    {
        //Take Health from enemy
        other.gameObject.GetComponent<Health>().TakeDamage(damage);
        //Destroy(gameObject);
    }
}
