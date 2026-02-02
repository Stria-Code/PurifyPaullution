using Unity.VisualScripting;
using UnityEngine;

public class PongBall : MonoBehaviour
{
    private int moveSpeed;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;
    public Sprite[] sprites;
    private Collider2D col;
    private bool hasCollided;
    private float collisionTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveSpeed = 5;
        hasCollided = false;
        collisionTime = 0;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (sprites.Length > 0)
        {
            sr.sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasCollided)
        {
            MoveBall();
        }

        if (hasCollided && collisionTime > 0 && Time.time >= collisionTime + 3f)
        {
            Destroy(rb.GameObject());
        }
    }

    void MoveBall()
    {
        rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        hasCollided = true;

        if (collision.gameObject.CompareTag("Player"))
        {
            collisionTime = Time.time;

            // Randomize y velocity within a range
            float newY = Random.Range(-3f, 3f);

            rb.linearVelocity = new Vector2(moveSpeed, newY);
            col.enabled = false;

            //animator.SetTrigger("Delete");
        }

        // Bounce off of walls
        // if (collision.gameObject.CompareTag("Wall"))
        // {
        //    rb.linearVelocity = new Vector2(rb.linearVelocity.x, -rb.linearVelocity.y);
        // }
    }
}
