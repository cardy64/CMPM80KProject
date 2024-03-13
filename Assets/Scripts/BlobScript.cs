using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobScript : MonoBehaviour
{

    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.Rotate(0, 0, Random.Range(0f, 360f));
    }
}
