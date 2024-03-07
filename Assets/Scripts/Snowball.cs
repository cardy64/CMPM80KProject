using UnityEngine;

public class Snowball : MonoBehaviour
{
    public float startHealth = 10f; // Time in seconds before health runs out
    public float maxHealth = 30f;
    public float ballSize = 1f;
    private float health;

    public BoxCollider2D ballCollider; // Reference to the snowball's collider
    public Transform ballTransform; // Reference to the snowball's transform

    void Start()
    {
        health = startHealth;
    }

    void FixedUpdate()
    {
        health -= Time.fixedDeltaTime;
        if (health < 0)
            health = 0;
        float scale = ballSize * health / maxHealth;
        ballTransform.localScale = Vector2.one * scale;
        ballTransform.localPosition = Vector2.up * scale/2 + Vector2.up/2;
    }
}
