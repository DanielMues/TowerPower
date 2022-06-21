using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int livePoints = 50;
    private int maxLivePoints;
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

    public void decreaseLive(int amount)
    {
        livePoints -= amount;
        float divide = ((float)livePoints / (float)maxLivePoints);
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
        foreach (GameObject player in customData.players)
        {
            if(currentPlayer.name != player.name)
            {
                player.GetComponent<Resources>().increaseGold(this.GetComponent<UnitStats>().returnValue);
            }
        }
        Destroy(this.gameObject);
    }

    public void Win()
    {
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
