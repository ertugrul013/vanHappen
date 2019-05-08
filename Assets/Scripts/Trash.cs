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
    
    /// <summary>
    /// used for determing when the object has spawned and pickedup
    /// </summary>
    /// <returns>current time</returns>
    public float SetTime()
    {
        return (Time.time);
    }
}
