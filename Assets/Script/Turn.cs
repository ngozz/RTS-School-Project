using UnityEngine;

public class Turn : MonoBehaviour
{
    private bool facingRight = true;
    private float lastX;

    void Start()
    {
        // Initialize lastX with the enemy's starting x position
        lastX = transform.position.x;
    }

    void Update()
    {
        // Check if the enemy's x position has decreased (moving left)
        if (transform.position.x < lastX && facingRight)
        {
            Flip();
        }
        // Check if the enemy's x position has increased (moving right)
        else if (transform.position.x > lastX && !facingRight)
        {
            Flip();
        }

        // Update lastX with the enemy's current x position
        lastX = transform.position.x;
    }

    void Flip()
    {
        // Switch the way the enemy is labelled as facing
        facingRight = !facingRight;

        // Multiply the enemy's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
