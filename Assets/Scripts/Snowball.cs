using Unity.Mathematics;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    public float startHealth = 10f; // Time in seconds before health runs out
    public float ballSize = 1f;
    public float startSizeMod = 0f;
    public float meltSpeed = 1;
    public float health;

    public BoxCollider2D ballCollider; // Reference to the snowball's collider
    public Transform ballTransform; // Reference to the snowball's transform

    GameObject player;
    PlayerMovement playerMovement;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerMovement = player.GetComponent<PlayerMovement>();
        health = startHealth;
    }

    void FixedUpdate()
    {
        health -= Time.fixedDeltaTime * meltSpeed;
        if (health < 0)
            health = 0;
        float radius = startSizeMod + ballSize * math.pow(3f * health / 4f / math.PI, 1f/3f);
        ballTransform.localScale = Vector2.one * radius;
        ballTransform.localPosition = Vector2.up * radius + Vector2.up * 1/2f;
    }

    void Update()
    {
        playerMovement.scrollBall(ballTransform.localScale.magnitude);
    }

    public void addSnow(float snow) {
        health += snow;
    }
}
