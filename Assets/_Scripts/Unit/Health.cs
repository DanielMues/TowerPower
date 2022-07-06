using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float livePoints = 50;
    public float protection = 0;
    public float fireProtection = 0;
    public float nuclearProtection = 0;
    public float electricProtection = 0;
    private float maxLivePoints;
    CustomDataStorage customData;
    public HealthBar healthBar;
    // Start is called before the first frame update

    private void Start()
    {
        customData = CustomDataStorage.instance;
        maxLivePoints = livePoints;
        healthBar.setSize(1f);
    }

    private void Update()
    {
        checkIfAlive();
    }

    public void decreaseLive(int amount, string damageType)
    {
        switch (damageType)
        {
            case "Normal":
                livePoints -= amount * (1 - protection);
                break;
            case "Fire":
                livePoints -= amount * (1 - fireProtection) * livePoints;
                break;
            case "Nuclear":
                livePoints -= amount * 1 - protection;
                break;
            case "Electric":
                livePoints -= amount * (1 - electricProtection) * maxLivePoints;
                break;
        }
        
        float divide = (livePoints / maxLivePoints);
        healthBar.setSize(divide);
    }

    private void checkIfAlive()
    {
        if (livePoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject currentPlayer = this.GetComponent<UnitStats>().getPlayer();
        currentPlayer.GetComponent<Resources>().increaseGold(this.GetComponent<UnitStats>().returnValue);
        Destroy(this.gameObject);
    }

    public void Win()
    {
        GameObject currentPlayer = this.GetComponent<UnitStats>().getPlayer();
        currentPlayer.GetComponent<PlayerHealth>().DecreaseLivePoints(1);
        Destroy(this.gameObject);
    }

    public bool destroyChildrenOnDeath = true;
    public static bool quitting = false;
    private void OnDestroy()
    {
        if (destroyChildrenOnDeath && !quitting)
        {
            int childCount = transform.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                GameObject childObject = transform.GetChild(i).gameObject;
                if (childObject != null)
                {
                    Destroy(childObject);
                }
            }
        }
        transform.DetachChildren();
    }
}
