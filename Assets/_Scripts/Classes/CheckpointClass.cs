using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CheckpointClass
{
    public int x;
    public int y;

    public CheckpointClass(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
