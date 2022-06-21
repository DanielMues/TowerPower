using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [Header("Unit attributes")]
    public float speed = 10f;

    private Grid tempgrid;
    private CheckpointClass targetCheckpoint;
    private Vector3 targetPosition;
    private Vector3 gridOriginPosition;
    CustomDataStorage customData;
    // Start is called before the first frame update
    void Start()
    {
        customData = CustomDataStorage.instance;
        CheckpointClass firstCheckpoint = customData.createPathCheckpoints[0];
        targetCheckpoint = customData.createPathCheckpoints[1];
        float closestX = this.transform.position.x - customData.originPositions[0].x;
        float closestY = this.transform.position.y - customData.originPositions[0].y;
        gridOriginPosition = customData.originPositions[0];
        int playerNumber = 0;
        int currentPlayer = playerNumber;
        foreach (Vector3 originPosition in customData.originPositions)
        {
            float xDifference = this.transform.position.x - originPosition.x;
            float yDifference = this.transform.position.y - originPosition.y;
            if ( (xDifference <= closestX && xDifference >= 0) && (yDifference <= closestY && yDifference >= 0))
            {
                closestX = xDifference;
                closestY = yDifference;
                gridOriginPosition = originPosition;
                currentPlayer = playerNumber;
            }
            playerNumber++;
        }
        this.GetComponent<UnitStats>().setPlayer(customData.players[currentPlayer]);
        tempgrid = new Grid(customData.gridWidth, customData.gridHeight, customData.cellSize, gridOriginPosition);
        targetPosition = tempgrid.GetCenteredWorldPosition(targetCheckpoint.x, targetCheckpoint.y);
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            for (int a = 0; a < customData.createPathCheckpoints.Count - 1; a++)
            {
                if (customData.createPathCheckpoints[a].x == targetCheckpoint.x && customData.createPathCheckpoints[a].y == targetCheckpoint.y)
                {
                    targetCheckpoint = customData.createPathCheckpoints[a + 1];
                    targetPosition = tempgrid.GetCenteredWorldPosition(customData.createPathCheckpoints[a + 1].x, customData.createPathCheckpoints[a + 1].y);
                    break;
                }
            }
            if (transform.position == tempgrid.GetCenteredWorldPosition(customData.createPathCheckpoints[customData.createPathCheckpoints.Count - 1].x, customData.createPathCheckpoints[customData.createPathCheckpoints.Count - 1].y))
            {
                this.GetComponent<Health>().Win();
            }
        }
    }
}
