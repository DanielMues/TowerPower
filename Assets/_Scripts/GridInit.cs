using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class GridInit : MonoBehaviour
{
    private Grid grid;
    // Start is called before the first frame update
    private CustomEventHandler customEventHandler;
    private CustomDataStorage customDataStorage;

    private int gridWidth;
    private int gridHeight;
    private float cellSize;
    private Vector3 originPosition;

    [Header("TileMap")]
    public Tilemap map;
    public List<Sprite> tiles;

    private List<CheckpointClass> createPathCheckpoints;
    private List<Vector3> originPositions;

    private const int GridEmptyValue = 0;
    private const int GridTurretValue = 1;
    private const int GridPathValue = 2;

    private GameObject selectedTurret;

    private List<GameObject>players;

    private void Start()
    {
        customEventHandler = CustomEventHandler.instance;
        customDataStorage = CustomDataStorage.instance;
        gridWidth = customDataStorage.gridWidth;
        gridHeight = customDataStorage.gridHeight;
        cellSize = customDataStorage.cellSize;
        createPathCheckpoints = customDataStorage.createPathCheckpoints;
        players = customDataStorage.players;
        int playerNumber = 0;
        foreach(GameObject player in players)
        {
            if(player.name == this.name)
            {
                originPosition = customDataStorage.originPositions[playerNumber];
                break;
            }
            playerNumber++;
        }
        grid = new Grid(gridWidth, gridHeight, cellSize, originPosition);
        Mesh gridMesh = grid.createMeshGrid();
        GetComponent<MeshFilter>().mesh = gridMesh;
        createPath(grid);
        SetTile(grid);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && selectedTurret != null)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (grid.GetValue(worldPosition) == GridEmptyValue)
            {
                Vector3 centerdGridFieldWorldPosition = grid.CenteredGridFieldWorldPosition(worldPosition);
                customEventHandler.SendSpawnTurret(players[0],centerdGridFieldWorldPosition, selectedTurret);
                grid.SetValue(worldPosition, GridTurretValue);
                selectedTurret = null;
            }
        }
    }


    // Create Map
    
    public void SetTile(Grid selectedGrid)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for(int y = 0; y < gridHeight; y++)
            {
                int tileValue = selectedGrid.GetValue(x, y);
                Vector3Int tilePosition = selectedGrid.GetIntWorldPosition(x, y);
                Tile currentTile = ScriptableObject.CreateInstance<Tile>();
                if(tileValue == GridEmptyValue)
                {
                    currentTile.sprite = tiles[GridEmptyValue];
                    map.SetTile(tilePosition, currentTile);
                }
                else if( tileValue == GridPathValue)
                {
                    currentTile.sprite = tiles[GridPathValue];
                    map.SetTile(tilePosition, currentTile);
                }
                else
                {
                    Debug.Log("Tilevalue has no sprite");
                }
            }
        }
    }

    // create Path on Map
    private void createPath(Grid selectedGrid)
    {
        List<CheckpointClass> fullPath = new List<CheckpointClass>();
        CheckpointClass lastCheckpoint = null;
        if (createPathCheckpoints != null)
        {
            foreach (CheckpointClass point in createPathCheckpoints)
            {
                if (lastCheckpoint != null)
                {
                    int newPointX = point.x;
                    int newPointY = point.y;
                    int lastPointX = lastCheckpoint.x;
                    int lastPointY = lastCheckpoint.y;

                    if (lastPointX == newPointX || lastPointY == newPointY)
                    {
                        if (newPointX >= 0 && newPointX <= gridWidth && newPointY >= 0 && newPointY <= gridHeight)
                        {
                            if (lastPointX == newPointX)
                            {
                                int differenzY = lastPointY - newPointY;
                                if (differenzY < 0)
                                {
                                    for (int a = -1; a > differenzY; a--)
                                    {
                                        fullPath.Add(new CheckpointClass(lastPointX, lastPointY - a));
                                    }
                                }
                                else if (differenzY > 0)
                                {
                                    for (int a = 1; a < differenzY; a++)
                                    {
                                        fullPath.Add(new CheckpointClass(lastPointX, lastPointY - a));
                                    }
                                }

                            }
                            else if (lastPointY == newPointY)
                            {
                                int differenzX = lastPointX - newPointX;
                                if (differenzX < 0)
                                {
                                    for (int a = -1; a > differenzX; a--)
                                    {
                                        fullPath.Add(new CheckpointClass(lastPointX - a, lastPointY));
                                    }
                                }
                                else if (differenzX > 0)
                                {
                                    for (int a = 1; a < differenzX; a++)
                                    {
                                        fullPath.Add(new CheckpointClass(lastPointX - a, lastPointY));
                                    }
                                }
                            }
                            fullPath.Add(point);
                            lastCheckpoint = point;
                        }
                        else
                        {
                            Debug.Log("Checkpoint out of bounds");
                            break;
                        }
                    }
                    else
                    {
                        Debug.Log("invalid Checkpoint");
                        break;
                    }
                }
                else
                {
                    if (point.x >= 0 && point.x <= gridWidth && point.y >= 0 && point.y <= gridHeight)
                    {
                        fullPath.Add(point);
                        lastCheckpoint = point;
                    }
                    else
                    {
                        Debug.Log("Checkpoint out of bounds");
                        break;
                    }
                }
            }
            foreach (CheckpointClass point in fullPath)
            {
                selectedGrid.SetValue(point.x, point.y, GridPathValue);
            }
        }
        else
        {
            Debug.Log("Not Path Set");
        }
    }

    public void setTurret(GameObject turret)
    {
        selectedTurret = turret;
    }

    public Grid getGrid()
    {
        return grid;
    }
}


