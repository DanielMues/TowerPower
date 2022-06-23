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
        GameObject[] overlays = GameObject.FindGameObjectsWithTag("Overlay");
        // disable not needed inGameOverlays
        foreach ( GameObject overlay in overlays)
        {
            overlay.SetActive(true);
        }
        if(inGameOverlays != null)
        {
            foreach(GameObject canvases in inGameOverlays)
            {
                canvases.SetActive(false); 
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
    }
}
