using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private int[,] gridArray;
    private Vector3 originPosition;

    //defines
    private const int GridEmptyValue = 0;

    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
    }

    public Mesh createMeshGrid()
    {
        gridArray = new int[width, height];

        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[4 * width * height];
        Vector2[] uv = new Vector2[4 * width * height];
        int[] triangles = new int[6 * width * height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int index = x * height + y;
                vertices[index * 4 + 0] = new Vector3(cellSize * x, cellSize * y);
                vertices[index * 4 + 1] = new Vector3(cellSize * x, cellSize * (y + 1));
                vertices[index * 4 + 2] = new Vector3(cellSize * (x + 1), cellSize * (y + 1));
                vertices[index * 4 + 3] = new Vector3(cellSize * (x + 1), cellSize * y);

                uv[index * 4 + 0] = new Vector2(0, 0);
                uv[index * 4 + 1] = new Vector2(0, 1);
                uv[index * 4 + 2] = new Vector2(1, 1);
                uv[index * 4 + 3] = new Vector2(1, 0);

                triangles[index * 6 + 0] = index * 4 + 0;
                triangles[index * 6 + 1] = index * 4 + 1;
                triangles[index * 6 + 2] = index * 4 + 2;

                triangles[index * 6 + 3] = index * 4 + 0;
                triangles[index * 6 + 4] = index * 4 + 2;
                triangles[index * 6 + 5] = index * 4 + 3;
                gridArray[x, y] = GridEmptyValue;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        return mesh;
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public Vector3 GetCenteredWorldPosition(int x, int y)
    {
        Vector3 worldPosition = new Vector3(x, y) * cellSize + originPosition;
        worldPosition.x += cellSize / 2;
        worldPosition.y += cellSize / 2;
        return worldPosition;
    }

    public Vector3Int GetIntWorldPosition(int x, int y)
    {
        return Vector3Int.FloorToInt(GetWorldPosition(x, y));
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition-originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition-originPosition).y / cellSize);
    }

    public void SetValue(int x, int y, int value)
    {
        if(x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y)
    {
        if(x >= 0 && y >= 0 && x < width && y < width)
        {
            return gridArray[x, y];
        }
        else
        {
            return -50;
        }
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    public Vector3 CenteredGridFieldWorldPosition(Vector3 currentWorldPosition)
    {
        int x, y;
        GetXY(currentWorldPosition, out x, out y);
        Vector3 worldPostion = GetWorldPosition(x, y);
        worldPostion.x += cellSize / 2;
        worldPostion.y += cellSize / 2;
        return worldPostion;
    }
}
