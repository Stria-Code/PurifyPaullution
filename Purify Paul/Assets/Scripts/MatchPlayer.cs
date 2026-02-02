using UnityEngine;

public class MatchPlayer : MonoBehaviour, IEntity
{
    private int moveSpeed;
    private int jumpForce;
    private bool isGrounded;
    private Rigidbody2D rb;
    private Transform playerTransform;
    private GameObject storedImage;
    private Vector2 positions;
    private SpriteRenderer sr;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        moveSpeed = 5;
        jumpForce = 7;
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveEntity();
    }

    virtual public void MoveEntity()
    {
        //If input A move player left
        if (Input.GetKey(KeyCode.A))
        {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
            sr.flipX = true;
            animator.SetBool("Running", true); //stuff for animator
        }


        //If input D move player left
        if (Input.GetKey(KeyCode.D))
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
            sr.flipX = false;
            animator.SetBool("Running", true);
        }

        //If no input don't move player
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animator.SetBool("Running", false);

        }

        //If input Space and not in air, jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("inAir", true);

        }
    }

    virtual public void OnCollisionEnter2D(Collision2D collision)
    {
        //Check Player collision with images
        //if yes
        //if input e
        //grab image
        //while image grabbed, if input e again
        //check if collision with another image
        //if image grabbed and other image are the same mark them as completed
        //else release image held

        if (storedImage)
        {
            if (collision.gameObject.CompareTag("Image"))
            {
                if (Input.GetKey(KeyCode.E))
                {
                    if (storedImage.GetComponent<SpriteRenderer>().color == collision.gameObject.GetComponent<SpriteRenderer>().color)
                    {
                        storedImage = null;
                        Destroy(storedImage);
                        Destroy(collision.gameObject);
                        Debug.Log("got the second image");

                    }
                }
            }
        }
        else if (collision.gameObject.CompareTag("Image") && !storedImage)
        {
            if (Input.GetKey(KeyCode.E))
            {
                storedImage = collision.gameObject.GetComponent<GameObject>();
                Debug.Log("got the first image");
            }
        }

        //add tag floor to all floors
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            animator.SetBool("inAir", false);
        }
    }
}
