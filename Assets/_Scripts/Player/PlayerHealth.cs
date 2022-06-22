using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int livePoints;


    public void Update()
    {
        if (CheckIfAlive())
        {
            LooseGame();
        }
    }

    private bool CheckIfAlive()
    {
        if(livePoints > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void LooseGame()
    {

    }

    public void DecreaseLivePoints(int amount)
    {
        livePoints -= amount;
    }
}
