using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class GroundGenerationScript : MonoBehaviour
{

    public Grid grid;
    public GameObject[] prefabs;

    private Camera camera;
    private List<String> touchedSpots = new List<String>();
    
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // camera.pixelWidth/2, 
        Vector3 worldPos = camera.ScreenToWorldPoint(new Vector3(-camera.pixelWidth/2, -camera.pixelHeight/2, camera.nearClipPlane));
        Vector3Int gridPos = grid.WorldToCell(worldPos);
        for (int x = 0; x < 7; x++) {
            for (int y = 1; y < 5; y++) {
                String str = "" + (gridPos.x + x) + "," + (gridPos.y + y);
                if (touchedSpots.Contains(str)) {
                    continue;
                }
                touchedSpots.Add(str);

                if (UnityEngine.Random.Range(0, 10) < 5) {
                    continue;
                }

                Vector3Int newPos = new Vector3Int(gridPos.x + x, gridPos.y + y, gridPos.z);
                Vector3 backToWorldPos = grid.CellToWorld(newPos);
                backToWorldPos.x += UnityEngine.Random.Range(-2, 2);
                backToWorldPos.y += UnityEngine.Random.Range(-2, 2);

                float randomAngle = UnityEngine.Random.Range(0f, 360f);
                Quaternion randomRotation = Quaternion.Euler(0, 0, randomAngle);

                Instantiate(prefabs[(int) UnityEngine.Random.Range(0, 2)], backToWorldPos, randomRotation);
            }
        }
        
    }
}
