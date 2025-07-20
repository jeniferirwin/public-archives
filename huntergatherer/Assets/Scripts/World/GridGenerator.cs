using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridGenerator : MonoBehaviour
{
    public Tilemap map;
    public Tile[] tiles;
    public int width;
    public int height;
    public float[] noise;
    
    public float[] thresholds;

    private void Start()
    {
        var total = width * height;
        noise = new float[total];
        int i = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var cell = new Vector3Int(x,y,Mathf.RoundToInt(map.transform.position.z));
                var point = map.GetCellCenterWorld(cell);
                noise[i] = Mathf.PerlinNoise(point.x, point.y);
                bool found = false;
                for (int k = 0; k < thresholds.Length; k++)
                {
                    if (noise[i] < thresholds[k])
                    {
                        map.SetTile(cell, tiles[k]);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    map.SetTile(cell, tiles[0]);
                }
                i++;
            }
        }
    }
}
