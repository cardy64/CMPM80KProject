using Unity.Mathematics;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public Sprite[] sprites;
    public float maxSpeed = 7f;
    public float acceleration = 1f;
    public float handling = 1f;
    public float brakePower = 5f;
    float angularVel = 0;
    float animTimer = 0;
    Vector2 linearVel = Vector2.zero;
    Vector2 input = Vector2.zero;

    public Rigidbody2D rb; // Reference to the Rigidbody2D component
    public SpriteRenderer sr; // Reference to the player's sprite renderer

    void Update()
    {
        // Input gathering
        input.x = Input.GetAxisRaw("Horizontal"); // Get horizontal input
        input.y = 1f + Input.GetAxisRaw("Vertical") * 4; // Get vertical input
        if (input.y < 0)
            input.y *= brakePower;
        
        // Update movement anims at a rate based on linear velocity
        if (animTimer >= (1f / linearVel.magnitude)) {
            animTimer = 0;
            sr.sprite = sprites[0];
            for (int i=0; i<sprites.Length-1; i++)
                sprites[i] = sprites[i+1];
            sprites[sprites.Length-1] = sr.sprite;
        }
        animTimer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        //update velocities
        angularVel += -input.x * handling * Time.fixedDeltaTime / (1 + linearVel.magnitude / 20);
        Vector2 forwardVector = new Vector2(math.cos(angularVel + math.PI/2), math.sin(angularVel + math.PI/2)).normalized;
        linearVel += forwardVector * input.y * acceleration * Time.fixedDeltaTime;

        //re-orient linear velocity
        float mag = linearVel.magnitude;
        linearVel = forwardVector * mag;

        //ensure true zero
        if (math.abs(angularVel) < 0.001)
            angularVel = 0;
        if (math.abs(linearVel.magnitude) < 0.001) {
            linearVel = Vector2.zero;
        }

        //update position
        rb.MovePosition(rb.position + linearVel * Time.fixedDeltaTime);
        rb.SetRotation(angularVel * 360 * 7/44);
    }
}
