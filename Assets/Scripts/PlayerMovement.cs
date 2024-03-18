using Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    private bool moving = false;

    public Sprite[] sprites;
    public float maxSpeed = 7f;
    public float acceleration = 1f;
    public float handling = 1f;
    public float brakePower = 5f;
    public float objectWorth = 2f;
    public float hitPenaltyMod = 2f;
    public Material ballMaterial;

    public float stillZoom = 8f;
    public float zoomMultiplier = 0.4f;
    public float changeSpeed = 0.02f;
    float zoomGoal;

    public GameObject startScreen;
    public GameObject endScreen;

    public CinemachineVirtualCamera cvc;

    public AudioSource bgMusic;

    float angularVel = 0;
    float animTimer = 0;
    Vector2 linearVel = Vector2.zero;
    Vector2 input = Vector2.zero;

    public Rigidbody2D rb; // Reference to the Rigidbody2D component
    public SpriteRenderer sr; // Reference to the player's sprite renderer

    Snowball snowballScript;

    void Start()
    {
        snowballScript = gameObject.GetComponentInChildren<Snowball>();

        zoomGoal = stillZoom;

        ballMaterial.mainTextureOffset = Vector2.zero;
    }

    void Update()
    {
        // Input gathering
        input.x = Input.GetAxisRaw("Horizontal"); // Get horizontal input
        // input.y = 1f + Input.GetAxisRaw("Vertical") * 4; // Get vertical input

        // if (input.x != 0) {
        //     moving = true;
        // }

        // if (!moving) {
        //     return;
        // }

        input.y = 4f;
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

        // Match camera zoom to linear velocity
        zoomGoal = stillZoom + linearVel.magnitude * zoomMultiplier;

        if (cvc.m_Lens.OrthographicSize > zoomGoal) {
            cvc.m_Lens.OrthographicSize -= changeSpeed;
            if (cvc.m_Lens.OrthographicSize < zoomGoal) {
                cvc.m_Lens.OrthographicSize = zoomGoal;
            }
        }

        if (cvc.m_Lens.OrthographicSize < zoomGoal) {
            cvc.m_Lens.OrthographicSize += changeSpeed;
            if (cvc.m_Lens.OrthographicSize > zoomGoal) {
                cvc.m_Lens.OrthographicSize = zoomGoal;
            }
        }

        // Match bg music speed to linear velocity
        bgMusic.pitch = (linearVel.magnitude * 0.01f) + 1f;

        if (snowballScript.health <= 0) {
            linearVel.x = 0;
            linearVel.y = 0;

            endScreen.SetActive(true);
        }
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

    public void hitObject(bool addSnow) {
        if (!addSnow) {
            linearVel /= 5;
            snowballScript.addSnow(-objectWorth*hitPenaltyMod);
        } else {
            snowballScript.addSnow(objectWorth);
        }
    }

    public void scrollBall(float ballSize) {
        // Scroll ball texture
        if (ballSize > 0) {
            ballMaterial.mainTextureOffset += Vector2.down * linearVel.magnitude / (ballSize*2*math.PI) * Time.fixedDeltaTime;
        }
    }
}
