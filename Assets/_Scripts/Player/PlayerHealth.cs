using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int livePoints;
    CustomEventHandler customEvent;
    CustomDataStorage customData;

    public void Start()
    {
        customEvent = CustomEventHandler.instance;
        customEvent.Defeat += EndTheGame;
        customData = CustomDataStorage.instance;
    }

    public void Update()
    {
        if (CheckIfAlive())
        {
            customEvent.SendDefeat(this.gameObject);
        }
    }

    private bool CheckIfAlive()
    {
        if(livePoints <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DecreaseLivePoints(int amount)
    {
        livePoints -= amount;
    }

    private void EndTheGame(object sender, CustomEventHandler.DefeatArguments args)
    {
        if(customData.players.Count == 2 && this.gameObject == customData.players[0])
        {
            GameObject parentOverlay = GameObject.Find("AllGameOverlay");
            GameObject endScreen = null;
            GameObject ingameScreen = null;
            foreach (Transform child in parentOverlay.GetComponentInChildren<Transform>())
            {
                if(child.name == "EndGameOverlay")
                {
                    endScreen = child.gameObject;
                }
                else if (child.name == "InGameOverlay") {
                    ingameScreen = child.gameObject;
                }
            }
            if(endScreen != null && ingameScreen != null)
            {
                Debug.Log("es ist vorhanden");
                endScreen.SetActive(true);
                ingameScreen.SetActive(false);
                GameObject endScreenText = GameObject.Find("EndText");
                if (args.player == this.gameObject)
                {
                    endScreenText.GetComponent<Text>().text = "Defeat";
                }
                else
                {
                    endScreenText.GetComponent<Text>().text = "Victory";
                }
            }
            else
            {
                Debug.Log("something is null");
                Debug.Log(endScreen);
                Debug.Log(ingameScreen);
            }
        }
    }
}
