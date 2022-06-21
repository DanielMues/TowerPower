using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public int spaceTaken;
    public int payCost;
    public int returnValue;
    private GameObject player;

    public void setPlayer(GameObject player)
    {
        this.player = player;
    }

    public GameObject getPlayer()
    {
        return this.player;
    }
}
