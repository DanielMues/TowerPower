using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnTurret : MonoBehaviour
{
    private CustomEventHandler customEventHandler;
    // Start is called before the first frame update
    private void Start()
    {
        customEventHandler = CustomEventHandler.instance;
        customEventHandler.SpawnTurret += SpawnTurretEvent;
    }

    private void SpawnTurretEvent(object sender, CustomEventHandler.SpawnTurrtArguments args)
    {
        if (args.player.GetComponent<Resources>().decreaseGold(args.turret.GetComponent<GameObjectResources>().goldCost))
        {
            GameObject.Instantiate(args.turret, new Vector3(args.worldPosition.x, args.worldPosition.y, 5), Quaternion.identity);
        }
        
    }
}
