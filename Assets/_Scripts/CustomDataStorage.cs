using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomDataStorage : MonoBehaviour
{

    [Header("gridvalues")]
    public int gridWidth;
    public int gridHeight;
    public float cellSize;
    public List<Vector3> originPositions;

    [Header("Path")]
    [SerializeField]
    public List<CheckpointClass> createPathCheckpoints;

    [Header("Players")]
    [SerializeField]
    public List<GameObject> players;

    public static CustomDataStorage instance;
    private void Start()
    {
        instance = this;
        if (gridWidth == 0)
        {
            Debug.Log("no gridWith set");
        }
        if (gridHeight == 0)
        {
            Debug.Log("no gridHeight set");
        }
        if (cellSize == 0)
        {
            Debug.Log("no cellSize set");
        }
        if (originPositions.Count == 0)
        {
            Debug.Log("no originPositions set");
        }
        if (players.Count == 0)
        {
            Debug.Log("no players set");
        }
        if (players.Count != originPositions.Count)
        {
            Debug.Log("players and gridOrigins dont have the same amount");
        }
    }

    
}
