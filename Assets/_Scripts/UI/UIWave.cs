using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWave : MonoBehaviour
{

    public Text maxRunnersAmount;
    public Text[] UnitText;
    CustomEventHandler customEventHandler;

    private void Start()
    {
        customEventHandler = CustomEventHandler.instance;
        customEventHandler.DisplayMaxRunners += DisplayMaxRunnersAmount;
        customEventHandler.DisplayUnitAmount += DisplayUnitAmount;

    }

    private void DisplayMaxRunnersAmount(object sender, CustomEventHandler.DisplayMaxRunnersArguments args)
    {
        maxRunnersAmount.text = string.Format("{0}/{1}", args.currentRunnerSpace, args.maxRunnerSpace);
    }

    private void DisplayUnitAmount(object sender, CustomEventHandler.DisplayUnitAmountsArguments args)
    {
        Text wantedText = FindUnitText(args.name);
        wantedText.text = string.Format("{0}", args.amount);
    }

    private Text FindUnitText(string textName)
    {
        foreach (Text text in UnitText)
        {
            if(text.name == textName)
            {
                return text;
            }
        }
        return null;
    }
}
