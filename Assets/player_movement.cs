using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed
    public float climbSpeed = 3f; // Speed for climbing

    private Rigidbody2D rb;
    private bool isClimbing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(horizontalMove, verticalMove);

        if (!isClimbing)
        {
            rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, movement.y * climbSpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check collision with walls to switch between walking and climbing
        if (collision.gameObject.CompareTag("Wall"))
        {
            isClimbing = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Exit climbing mode when not in contact with the wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            isClimbing = false;
        }
    }
}

