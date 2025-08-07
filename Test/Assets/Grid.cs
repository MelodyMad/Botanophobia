using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
 
    public float waterLevel = 0.4f;
    public int size = 10;
    public float scale = 0.1f;
    float xOffset;
    float yOffset;

    Cell[,] grid;

    void Start()
    {
        xOffset = Random.Range(-10000f, 10000f);
        yOffset = Random.Range(-10000f, 10000f);

        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new Cell[size, size];

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noiseValue = Mathf.PerlinNoise((x + xOffset) * scale, (y + yOffset) * scale);


                Cell cell = new Cell();
                cell.isWater = noiseValue < waterLevel;
                grid[x, y] = cell;
            }
        }
    }

    void OnDrawGizmos()
    {
        if (grid == null)
        {
            xOffset = 0f; // keep it fixed in editor mode
            yOffset = 0f;
            GenerateGrid();
        }

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cell cell = grid[x, y];
                Gizmos.color = cell.isWater ? Color.blue : Color.green;
                Vector3 pos = new Vector3(x, 0, y);
                Gizmos.DrawCube(pos, Vector3.one * 0.95f);
            }
        }
    }
}
