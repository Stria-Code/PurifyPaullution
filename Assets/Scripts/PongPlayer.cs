using UnityEngine;

public class PongPlayer : MonoBehaviour, IEntity
{
    private int moveSpeed;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        MoveEntity();
    }

    virtual public void MoveEntity()
    {
        //If input W move player up
        if (Input.GetKey(KeyCode.W))
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.x);
        }

        //If input S move player down
        if (Input.GetKey(KeyCode.S))
        {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.x);
        }

        //If no input don't move player
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.x);
        }
    }

    virtual public void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
