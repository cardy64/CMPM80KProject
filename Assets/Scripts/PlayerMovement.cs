using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxAcc = 7;
    public float maxSpeed = 7;
    public float maxTurnSpeed = 3.5f; // in rads per second
    public float AngularAcc = 30; //rads 
    public float LinearAcc = 15;
    float speed = 0;
    float anuglarVel = 0;
    float speedCap = 8;
    float rotation = 0.0f;
    public float LengthwiseTriction = 0.5f;
    public float TransverseFriction = 0.9f;
    public float turnFriction = 0.5f;

    Vector2 vel = new Vector2(0f, 0f);
    Vector2 movement = new Vector2(0.0f, 0.0f);


    public Rigidbody2D rb; // Reference to the Rigidbody2D component

    void Start()
    {
        Debug.Log("hello");
    }

    void Update()
    {
        // Input gathering
        movement.x = Input.GetAxisRaw("Horizontal"); // Get horizontal input
        movement.y = Input.GetAxisRaw("Vertical"); // Get vertical input

        
    }

    void FixedUpdate()
    {


        //apply friction
        if (vel.magnitude > 0) {
            vel -= vel.normalized * LengthwiseTriction * LinearAcc * Time.fixedDeltaTime;
            anuglarVel -= math.sign(anuglarVel) * AngularAcc * turnFriction * Time.fixedDeltaTime;
        }

        //update velocities
        anuglarVel += -movement.x * AngularAcc * Time.fixedDeltaTime;
        vel.x += math.cos(rotation) * movement.y * LinearAcc * Time.fixedDeltaTime;
        vel.y += math.sin(rotation) * movement.y * LinearAcc * Time.fixedDeltaTime;

        //clamp velocities
        float cappedTurnSpeed = maxTurnSpeed * vel.magnitude;
        vel = vel.normalized * math.clamp(vel.magnitude, 0, speedCap);
        anuglarVel = math.clamp(anuglarVel, -cappedTurnSpeed, cappedTurnSpeed);

        //ensure true zero
        if (math.abs(anuglarVel) < 0.1)
            anuglarVel = 0;
        
        if (vel.magnitude < 0.1) {
            vel.x = 0;
            vel.y = 0;
        }

        //update position
        rb.MovePosition(rb.position + vel * Time.fixedDeltaTime);
        rb.SetRotation(rotation * 360 * 7/44);
        rotation += anuglarVel * Time.fixedDeltaTime;

        // Movement execution
        //rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }
}
