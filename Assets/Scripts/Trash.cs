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

	private bool isSpawned;

	public Type mytype;
	public float spawnTime, pickupTime;

	public void SetTime()
	{
		if (isSpawned)
		{
			pickupTime = Time.time - spawnTime;
		}
		else if (!isSpawned)
		{
			spawnTime = Time.time;
			isSpawned = true;
		}
	}
}