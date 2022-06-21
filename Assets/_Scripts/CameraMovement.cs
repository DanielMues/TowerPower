using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float zoomStep, minCamSize, maxCamSize;

    private Vector3 dragOrigin;

    private float mapMinX, mapMaxX, mapMinY, mapMaxY;
    private CustomDataStorage customDataStorage;

    private void Start()
    {
        customDataStorage = CustomDataStorage.instance;
        if (customDataStorage != null)
        {
            int width = customDataStorage.gridWidth;
            int height = customDataStorage.gridHeight;
            float cellSize = customDataStorage.cellSize;
            Vector3 originPosition = customDataStorage.originPositions[0];

            mapMinX = originPosition.x;
            mapMaxX = originPosition.x + width * cellSize;

            mapMinY = originPosition.y;
            mapMaxY = originPosition.y + height * cellSize;
        }
        else
        {
            Debug.Log("No dataStorage for camera found");
        }
        if (cam != null)
        {
            float camHeight = cam.orthographicSize;
            float camWidth = cam.orthographicSize * cam.aspect;
            cam.transform.position += new Vector3(camWidth, camHeight, cam.transform.position.z);
        }
        else
        {
            Debug.Log("No cam installed!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PanCamera();
    }

    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }
    }

    public void ZoomIn()
    {
        float newSize = cam.orthographicSize + zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

        cam.transform.position = ClampCamera(cam.transform.position);
    }

    public void ZoomOut()
    {
        float newSize = cam.orthographicSize - zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

        cam.transform.position = ClampCamera(cam.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
