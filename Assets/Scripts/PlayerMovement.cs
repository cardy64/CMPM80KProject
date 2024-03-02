using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player
    public Rigidbody2D rb; // Reference to the Rigidbody2D component

    private Vector2 movement; // The movement vector

    void Update()
    {
        // Input gathering
        movement.x = Input.GetAxisRaw("Horizontal"); // Get horizontal input
        movement.y = Input.GetAxisRaw("Vertical"); // Get vertical input
    }

    void FixedUpdate()
    {
        // Movement execution
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
