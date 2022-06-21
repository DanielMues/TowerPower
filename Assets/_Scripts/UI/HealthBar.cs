using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform bar;

    public void setSize(float size)
    {
        bar.localScale = new Vector3(size, 1f);
    }
}
