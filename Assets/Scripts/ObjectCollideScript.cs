using UnityEngine;

public class ObjectCollideScript : MonoBehaviour
{

    public bool addSnow;
    public bool rotate;
    public AudioClip[] breakSfx;

    private AudioSource audioSource;
    GameObject player;
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerMovement = player.GetComponent<PlayerMovement>();
        audioSource = player.GetComponent<AudioSource>();
        audioSource.volume = 0.1f;

        float rotateAmount = 0;

        if (rotate) {
            rotateAmount = Random.Range(0f, 360f);
        }

        transform.Rotate(0, 0, rotateAmount);
    }

    void OnTriggerEnter2D(Collider2D collision2D)
    {
        Destroy(gameObject);

        playerMovement.hitObject(addSnow);

        audioSource.clip = breakSfx[Random.Range(0, breakSfx.Length-1)];
        audioSource.enabled = true;
        audioSource.Play();
    }
}
