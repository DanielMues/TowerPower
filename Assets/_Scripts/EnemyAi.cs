using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    private CustomDataStorage customData;
    // Start is called before the first frame update
    private Vector3 gridOriginPosition;
    private Grid playerGrid;
    private WaveHandler waveHandler;
    void Start() {
        playerGrid = this.GetComponent<GridInit>().getGrid();
        waveHandler = this.GetComponent<WaveHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(waveHandler.GetCurrentRunnerAmount() < waveHandler.GetRunnerListMax())
        {
            addRunners();
        }
    }

    private void addRunners()
    {
        float maxTank = 0.4f;
        float maxSpeedster = 0.4f;
        float maxAllrounder = 0.5f;

        GameObject tank = null;
        GameObject allrounder = null;
        GameObject speedster = null;

        foreach (GameObject runner in waveHandler.enemy)
        {
            if (runner.name == "Allrounder")
            {
                allrounder = runner;
            }
            else if (runner.name == "Speedster")
            {
                speedster = runner;
            }
            else if (runner.name == "Tank")
            {
                tank = runner;
            }
        }

        int tankAmount = (int)(waveHandler.GetRunnerListMax() / tank.GetComponent<UnitStats>().spaceTaken * maxTank);
        int speedsterAmount = (int)(waveHandler.GetRunnerListMax() / speedster.GetComponent<UnitStats>().spaceTaken * maxSpeedster);
        int allrounderAmount = (int)(waveHandler.GetRunnerListMax() / allrounder.GetComponent<UnitStats>().spaceTaken * maxAllrounder);

        for (int x = 0; x < tankAmount; x++)
        {
            waveHandler.addRunner(tank.name);
        }

        for (int x = 0; x < speedsterAmount; x++)
        {
            waveHandler.addRunner(speedster.name);
        }

        for (int x = 0; x < allrounderAmount; x++)
        {
            waveHandler.addRunner(allrounder.name);
        }

        if (waveHandler.GetCurrentRunnerAmount() < waveHandler.GetRunnerListMax())
        {
            int difference = waveHandler.GetRunnerListMax() - waveHandler.GetCurrentRunnerAmount();
            if (difference % allrounder.GetComponent<UnitStats>().spaceTaken == 0)
            {
                while (waveHandler.GetCurrentRunnerAmount() < waveHandler.GetRunnerListMax())
                {
                    waveHandler.addRunner(allrounder.name);
                }
            }
            else if (difference % speedster.GetComponent<UnitStats>().spaceTaken == 0)
            {
                while (waveHandler.GetCurrentRunnerAmount() < waveHandler.GetRunnerListMax())
                {
                    waveHandler.addRunner(speedster.name);
                }
            }
        }
    }
}
