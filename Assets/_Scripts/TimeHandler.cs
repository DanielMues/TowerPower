using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeHandler : MonoBehaviour
{
    [Tooltip("Text Field for Time")]
    public Text timeText;
    private float gameTime;
    public float waveTime = 30f;
    private float waveTimer;
    private CustomEventHandler customEventHandler;

    // Start is called before the first frame update
    void Start()
    {
        customEventHandler = CustomEventHandler.instance;
        gameTime = 0;
        waveTimer = waveTime;
        if (timeText != null)
        {
            float minutes = Mathf.FloorToInt(waveTimer / 60);
            float seconds = Mathf.FloorToInt(waveTimer % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        waveTimer -= Time.deltaTime;
        if (waveTimer < 0)
        {
            waveTimer = waveTime;
            customEventHandler.SendStartWave();
        }
        DisplayTime(waveTimer);
    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
