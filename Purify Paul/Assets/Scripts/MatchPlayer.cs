using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MatchPlayer : MonoBehaviour, IEntity
{
    private int moveSpeed;
    private int jumpForce;
    private bool isGrounded;
    private Rigidbody2D rb;
    private Transform playerTransform;
    private Vector2 positions;
    private SpriteRenderer sr;
    private Animator animator;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private float groundRadius;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LineRenderer linePrefab;
    private GameObject storedImage;
    private GameObject currentImage;
    int matches;
    int lives;
    MatchImagesManager manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = FindFirstObjectByType<MatchImagesManager>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        moveSpeed = 5;
        jumpForce = 7;
        matches = 0;
        lives = 3;
        isGrounded = true;
        groundRadius = 0.2f;
        lineRenderer.enabled = false;
        storedImage = null;
        currentImage = null;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        MoveEntity();
        UpdateLine();
        CheckImages();
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

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        animator.SetBool("inAir", false);

    }


    private void CheckImages()
    {
        if (currentImage != null && Input.GetKeyDown(KeyCode.E))
        {
            if (!storedImage)
            {
                // Grab the first image
                storedImage = currentImage;
                lineRenderer.enabled = true;
                Debug.Log("Got the first image");
            }
            else
            {
                // Compare second image
                if (storedImage.GetComponent<SpriteRenderer>().sprite == currentImage.GetComponent<SpriteRenderer>().sprite)
                {

                    if(storedImage == currentImage)
                    {
                        Debug.Log("Cannot match the same image!");
                        return;
                    }

                    LineRenderer newLine = Instantiate(linePrefab);
                    newLine.positionCount = 2;
                    newLine.SetPosition(0, storedImage.transform.position);
                    newLine.SetPosition(1, currentImage.transform.position);

                    lineRenderer.enabled = false;

                    storedImage.GetComponent<Collider2D>().enabled = false;

                    if (currentImage != null)
                    {
                        currentImage.GetComponent<Collider2D>().enabled = false;
                    }

                    storedImage = null;
                    matches++;
                    Debug.Log("Images matched!");
                }
                else
                {
                    lineRenderer.enabled = false;
                    storedImage = null;
                    lives--;
                    Debug.Log("Images did not match!");
                }
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Image"))
        {
            //track current image under player
            currentImage = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Image") && collision.gameObject == currentImage)
        {
            currentImage = null;
        }
    }

    private void UpdateLine()
    {
        if (storedImage)
        {
            // Line start = first image, end = player
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, storedImage.transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }
    }

    virtual public void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {

    }

    public int GetLives()
    {
        return lives;
    }

    public int GetMatches()
    {
        return matches;
    }


    public void SetLives(int l)
    {
        lives = l;
    }

    public void SetMatches(int m)
    {
        matches = m;
    }
}