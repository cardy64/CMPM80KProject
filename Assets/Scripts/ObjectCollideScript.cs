using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollideScript : MonoBehaviour
{

    public bool addsSnow;

    // Start is called before the first frame update
    void Start(){}

    void Update(){}

    void OnTriggerEnter2D(Collider2D collision2D)
    {
        Destroy(gameObject);
    }
}
