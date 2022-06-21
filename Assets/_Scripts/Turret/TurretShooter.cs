using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooter : MonoBehaviour
{

    public int damage = 1;
    public float timeBetweenShoot = 0.5f;
    private GameObject targetEnemy;
    private float lastshot;
    private float currenTime;
    // Start is called before the first frame update
    void Start()
    {
        currenTime = Time.deltaTime;
        lastshot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currenTime += Time.deltaTime;
        if (targetEnemy != null)
        {
            if (lastshot + timeBetweenShoot < currenTime)
            {
                Health runnerHealth = targetEnemy.GetComponent<Health>();
                runnerHealth.decreaseLive(damage);
                lastshot = currenTime;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (targetEnemy == null && collision.gameObject.tag == "Runner")
        {
            targetEnemy = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (targetEnemy == collision.gameObject)
        {
            targetEnemy = null;
        }
    }
}
