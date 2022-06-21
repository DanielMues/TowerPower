using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    [Header("Resources")]
    public int gold = 0;
    public int elixier = 0;
    public int elixierIncreasePerSecond = 0;

    private void Start()
    {
        currentTime = Time.deltaTime;
        lastTime = Time.deltaTime;
    }


    private float currentTime;
    private float lastTime;
    private void Update()
    {
        currentTime += Time.deltaTime;
        if (lastTime + 1f < currentTime)
        {
            elixier += elixierIncreasePerSecond;
            currentTime = Time.deltaTime;
        }
    }

    public int getGold()
    {
        return gold;
    }

    public int getElixier()
    {
        return elixier;
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

    public void increaseElixier(int amount)
    {
        elixier += amount;
    }

    public bool decreaseElixier(int amount)
    {
        if (checkIfEnoughElixierIsAvailable(amount))
        {
            elixier -= amount;
            return true;
        }
        else
        {
            Debug.Log("not enough elixier");
            return false;
        }
    }

    private bool checkIfEnoughElixierIsAvailable(int amount)
    {
        return (elixier - amount >= 0);
    }

    private bool checkIfEnoughGoldIsAvailable(int amount)
    {
        return (gold - amount >= 0);
    }
}
