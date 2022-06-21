using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInit : MonoBehaviour
{
    [Header("Other Canvases")]
    public GameObject[] inGameOverlays;
    [Header("Main UI Objects")]
    public Text goldText;
    public Text elixierText;
    private CustomDataStorage customDataStorage;
    // Start is called before the first frame update
    private void Start()
    {
        // disable not needed inGameOverlays
        if(inGameOverlays != null)
        {
            foreach(GameObject canvases in inGameOverlays)
            {
                canvases.GetComponent<Canvas>().enabled = false; 
            }
        }
        customDataStorage = CustomDataStorage.instance;
    }

    private void Update()
    {
        Resources playerResources = customDataStorage.players[0].GetComponent<Resources>();
        if(goldText != null)
        {
            int gold = playerResources.getGold();
            goldText.text = string.Format("Gold: {0}", gold);
        }
        else
        {
            Debug.Log("no gold text found");
        }
        if (elixierText != null)
        {
            int elixier = playerResources.getElixier();
            elixierText.text = string.Format("Elixier: {0}", elixier);
        }
        else
        {
            Debug.Log("no elixier text found");
        }
    }
}
