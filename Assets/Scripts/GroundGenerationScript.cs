using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GroundGenerationScript : MonoBehaviour
{

    public Grid mainGrid;
    private Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight, camera.nearClipPlane));
        print(p.x);
    }
}
