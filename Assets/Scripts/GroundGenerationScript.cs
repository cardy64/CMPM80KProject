using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundGenerationScript : MonoBehaviour
{
    public float spawnChance = 0.5f;
    public GameObject[] prefabs;
    List<GameObject> queue = new List<GameObject>();

    private Camera camera;
    private List<String> touchedSpots = new List<String>();

    private Grid grid;
    public int gridSize;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;

        grid = GetComponent<Grid>();
        gridSize = (int) grid.cellSize.x;
    }

    // Update is called once per frame
    void Update()
    {

        // camera.pixelWidth/2, 
        Vector3 worldPos = camera.ScreenToWorldPoint(new Vector3(-camera.pixelWidth/2, -camera.pixelHeight/2, camera.nearClipPlane));
        Vector3 worldPos2 = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth/2, camera.pixelHeight/2, camera.nearClipPlane));

        Vector3Int gridPos = grid.WorldToCell(worldPos);
        Vector3Int gridPos2 = grid.WorldToCell(worldPos2);

        for (int x = 0; x < (gridPos2.x - gridPos.x)*2+2; x++) {
            for (int y = 1; y < (gridPos2.y - gridPos.y)*2+2; y++) {
                String str = "" + (gridPos.x + x) + "," + (gridPos.y + y);
                if (touchedSpots.Contains(str)) {
                    continue;
                }
                touchedSpots.Add(str);
                if (gridPos.x + x == 0 &&
                    gridPos.y + y == 0) {
                    continue;
                }

                if (UnityEngine.Random.Range(0, 1) > spawnChance) {
                    // continue;
                }

                Vector3Int newPos = new Vector3Int(gridPos.x + x, gridPos.y + y, gridPos.z);
                Vector3 backToWorldPos = grid.CellToWorld(newPos);
                backToWorldPos.x += UnityEngine.Random.Range(-5, 5);
                backToWorldPos.y += UnityEngine.Random.Range(-5, 5);

                // float randomAngle = UnityEngine.Random.Range(0f, 360f);
                // Quaternion randomRotation = Quaternion.Euler(0, 0, randomAngle);
                Quaternion randomRotation = Quaternion.Euler(0, 0, 0);

                Instantiate(getRandomObject(), backToWorldPos, randomRotation);
            }
        }
    }

    GameObject getRandomObject() {
        if (queue.Count == 0) {
            for (int i = 0; i < prefabs.Length; i++) {
                for (int ii = 0; ii < 3; ii++) {
                    queue.Add(prefabs[i]);
                }
            }
            Shuffle(queue);
        }
        print(queue.Count);
        GameObject ret = queue[0];
        queue.RemoveAt(0);
        return ret;
    }

    private static System.Random rng = new System.Random();  

    public static void Shuffle(IList<GameObject> list)
    {
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            GameObject value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}