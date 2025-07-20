using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

[ExecuteInEditMode]
public class TilemapVariables : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private int width;
    [SerializeField] private int height;
    public Vector3Int[] cells;
    public CellData[] cellData;

    private void Start()
    {
        PopulateCellArray();
    }

    public void PopulateCellArray()
    {
        cells = new Vector3Int[width * height];
        cellData = new CellData[width * height];
        int i = 0;
        for (int y = 0; y > -width; y--)
        {
            for (int x = 0; x < height; x++)
            {
                var pos = new Vector3Int(x, y, 0);
                cells[i] = pos;
                cellData[i] = RandomizedCellData();
                i++;
            }
        }
    }

    public CellData RandomizedCellData()
    {
        return new CellData(Random.Range(1, 11));
    }
}
