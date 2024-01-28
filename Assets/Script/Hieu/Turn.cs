using UnityEngine;

public class Turn : MonoBehaviour
{
    private bool facingRight = true;
    private float lastX;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        lastX = transform.position.x;
        spriteRenderer = GetComponent<SpriteRenderer>();
        facingRight = transform.localScale.x > 0;
    }

    void Update()
    {
        float currentX = transform.position.x;

        if (currentX < lastX && facingRight || currentX > lastX && !facingRight)
        {
            Flip();
        }

        lastX = currentX;
    }

    void Flip()
    {
        facingRight = !facingRight;

        // Get the current local scale
        Vector3 theScale = transform.localScale;

        // Flip the x axis
        theScale.x *= -1;

        // Apply the new local scale
        transform.localScale = theScale;
    }
}