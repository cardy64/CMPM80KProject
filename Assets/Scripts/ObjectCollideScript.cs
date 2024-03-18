using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollideScript : MonoBehaviour
{

    public bool addSnow;
    public bool rotate;
    public AudioSource audio;

    GameObject player;
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerMovement = player.GetComponent<PlayerMovement>();

        GameObject audioSource = GameObject.FindGameObjectsWithTag("Respawn")[0];
        audio = audioSource.GetComponent<AudioSource>();

        float rotateAmount = 0;

        if (rotate) {
            rotateAmount = Random.Range(0f, 360f);
        }

        transform.Rotate(0, 0, rotateAmount);
    }

    void Update(){}

    void OnTriggerEnter2D(Collider2D collision2D)
    {
        Destroy(gameObject);

        playerMovement.hitObject(addSnow);

        audio.Play();
    }
}
