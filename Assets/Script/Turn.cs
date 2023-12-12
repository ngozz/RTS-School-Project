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
        facingRight = !spriteRenderer.flipX;
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
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}