using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveHandler : MonoBehaviour
{

    
    [Header("Enemy")]
    public List<GameObject> enemy;

    [Header("RunnerList")]
    private int runnerListMax;
    private int currentRunnerAmount;
    private List<GameObject> runners;
    // instances 
    CustomEventHandler customEventHandler;
    CustomDataStorage customDataStorage;
    private Vector3 spawnPoint;
    private CheckpointClass tempPoint;
    private GameObject player;
    private List<GameObject> players;
    // Start is called before the first frame update
    void Start()
    {
        runners = new List<GameObject>();
        runnerListMax = 2;
        currentRunnerAmount = 0;
        customEventHandler = CustomEventHandler.instance;
        customEventHandler.StartWave += SendWave;
        customDataStorage = CustomDataStorage.instance;
        tempPoint = customDataStorage.createPathCheckpoints[0];
        player = this.gameObject;
        GameObject enemy = GetEnemyPlayer(customDataStorage.players, player);
        spawnPoint = enemy.GetComponent<GridInit>().getGrid().GetCenteredWorldPosition(tempPoint.x, tempPoint.y);
        if(player.name == "Player1")
        {
            customEventHandler.SendDisplayMaxRunner(currentRunnerAmount, runnerListMax);
            ResetAllUnitAmountDisplay();
        }
    }

    // Spawns
    private void SendWave(object sender, EventArgs args)
    {
        if (runners.Count > 0)
        {
            float offset = 0;
            foreach (GameObject enemy in runners)
            {
                Instantiate(enemy, new Vector3(spawnPoint.x - offset, spawnPoint.y, 5), Quaternion.identity);
                offset += 0.5f;
            }
            runners.Clear();
            if (currentRunnerAmount >= runnerListMax * 0.8)
            {
                runnerListMax += 2;
            }
            currentRunnerAmount = 0;
            if (player.name == "Player1")
            {
                customEventHandler.SendDisplayMaxRunner(currentRunnerAmount, runnerListMax);
                ResetAllUnitAmountDisplay();
            }
        }
    }

    public void addRunner(string name)
    {
        GameObject enemy = findEnemy(name);
        if(currentRunnerAmount + enemy.GetComponent<UnitStats>().spaceTaken <= runnerListMax)
        {
            runners.Add(enemy);
            currentRunnerAmount += enemy.GetComponent<UnitStats>().spaceTaken;
            if(player.name == "Player1")
            {
                int enemyAmount = GetAmountOfAddedEnemies(enemy);
                customEventHandler.SendDisplayUnitAmount(enemyAmount, enemy.name);
                customEventHandler.SendDisplayMaxRunner(currentRunnerAmount, runnerListMax);
            }
        }
        else
        {
            Debug.Log("full");
        }
    }

    private GameObject findEnemy(string name)
    {
        foreach (GameObject obj in enemy)
        {
            if(name == obj.name)
            {
                return obj;
            }
        }
        return null;
    }

    private void ResetAllUnitAmountDisplay()
    {
        foreach(GameObject enemy in enemy)
        {
            customEventHandler.SendDisplayUnitAmount(0, enemy.name);
        }
    }

    private int GetAmountOfAddedEnemies(GameObject findEnemy)
    {
        int amount = 0;
        foreach (GameObject enemy in runners)
        {
            if(enemy == findEnemy)
            {
                amount += 1;
            }
        }
        return amount;
    }

    public int GetRunnerListMax()
    {
        return runnerListMax;
    }

    public int GetCurrentRunnerAmount()
    {
        return currentRunnerAmount;
    }

    private GameObject GetEnemyPlayer(List<GameObject> players, GameObject player)
    {
        GameObject enemyPlayer = null;
        int playerCount = 0;
        foreach (GameObject enemy in players)
        {
            if(enemy == player)
            {
                if(playerCount  == players.Count - 1)
                {
                    enemyPlayer = players[0]; 
                }
                else
                {
                    enemyPlayer = players[playerCount + 1];
                }
            }
            playerCount++;
        }
        return enemyPlayer;
    }
}
