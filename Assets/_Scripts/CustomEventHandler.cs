using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomEventHandler : MonoBehaviour
{
    public static CustomEventHandler instance;
    private void Start()
    {
        instance = this;
    }

    // custom Spawn Turret Event
    public event EventHandler<SpawnTurrtArguments> SpawnTurret;
    public class SpawnTurrtArguments : EventArgs
    {
        public GameObject player;
        public Vector3 worldPosition;
        public GameObject turret;
    }

    public void SendSpawnTurret(GameObject player, Vector3 worldPosition, GameObject turret)
    {
        SpawnTurret?.Invoke(this, new SpawnTurrtArguments {player = player, worldPosition = worldPosition, turret = turret });
    }

    //custom start wave Event
    public event EventHandler<EventArgs> StartWave;

    public void SendStartWave()
    {
        StartWave?.Invoke(this, new EventArgs());
    }

    // custom Display Max Runner 
    public event EventHandler<DisplayMaxRunnersArguments> DisplayMaxRunners;
    public class DisplayMaxRunnersArguments : EventArgs
    {
        public int currentRunnerSpace;
        public int maxRunnerSpace;
    }

    public void SendDisplayMaxRunner(int currentRunnerSpace, int maxRunnerSpace)
    {
        DisplayMaxRunners?.Invoke(this, new DisplayMaxRunnersArguments { currentRunnerSpace = currentRunnerSpace, maxRunnerSpace = maxRunnerSpace });
    }

    // custom Display Unit Amount
    public event EventHandler<DisplayUnitAmountsArguments> DisplayUnitAmount;
    public class DisplayUnitAmountsArguments : EventArgs
    {
        public int amount;
        public string name;
    }

    public void SendDisplayUnitAmount(int amount, string name)
    {
        DisplayUnitAmount?.Invoke(this, new DisplayUnitAmountsArguments { amount = amount, name = name });
    }
}
