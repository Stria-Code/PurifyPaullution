using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


public class Player : MonoBehaviour , IEntity
{
    private int moveSpeed;
    private int jumpForce;
    private bool isGrounded;
    private Rigidbody2D rb;
    private Transform playerTransform;
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
        if(Input.GetKey(KeyCode.A))
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

        //Check Player collision with image tag object
        //if yes call platformer load next scene.
        // PlatformerManager platformer;
        // platformer = GetComponent<PlatformerManager>();
        // platformer.OpenMatchingImagesMiniGame();

        //add tag floor to all floors
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }

        animator.SetBool("inAir", false);
    }
}
