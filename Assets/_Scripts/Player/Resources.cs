using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    [Header("Resources")]
    public int gold = 0;

    private void Start()
    {
        currentTime = Time.deltaTime;
        lastTime = Time.deltaTime;
    }

    private float currentTime;
    private float lastTime;

    public int getGold()
    {
        return gold;
    }

    public void increaseGold(int amount)
    {
        gold += amount;
    }

    public bool decreaseGold(int amount)
    {
        if (checkIfEnoughGoldIsAvailable(amount))
        {
            gold -= amount;
            return true;
        }
        else
        {
            Debug.Log("not enough gold");
            return false;
        }
    }

    private bool checkIfEnoughGoldIsAvailable(int amount)
    {
        return (gold - amount >= 0);
    }
}
