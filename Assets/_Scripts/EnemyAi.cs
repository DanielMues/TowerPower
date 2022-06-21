using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public GameObject player;
    private CustomDataStorage customData;
    // Start is called before the first frame update
    private Vector3 gridOriginPosition;
    private Grid playerGrid;
    private WaveHandler waveHandler;
    void Start() {
        playerGrid = player.GetComponent<GridInit>().getGrid();
        waveHandler = player.GetComponent<WaveHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(waveHandler.GetCurrentRunnerAmount() < waveHandler.GetRunnerListMax())
        {
            waveHandler.addRunner(waveHandler.enemy[0].name);
        }
    }
}
