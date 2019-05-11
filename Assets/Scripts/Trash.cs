using System;
using UnityEngine;

[Serializable]
public class Trash
{
    public enum Type
    {
        green,
        plastic,
        chemicals,
        paper
    }

    public Type mytype;
    public float spawnTime, pickupTime;
    private bool isSpawned = false;
    
    /// <summary>
    /// 
    /// </summary>
    public void SetTime()
    {
        if (isSpawned)
        {
            pickupTime = Time.time - spawnTime;
        }
        else if(!isSpawned)
        {
            spawnTime = Time.time;
            isSpawned = true;
        }
    }
}
