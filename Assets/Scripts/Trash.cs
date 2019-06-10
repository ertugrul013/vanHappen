using System;
using UnityEngine;

[Serializable]
public class Trash
{
	private static float startTime;
	public Guid GUID;
	public Type mytype;
	public float pickUpTime;

	public int amountOfScore;

	public void SetTime()
	{
		pickUpTime = Time.time - startTime;
	}

	public void GetFirstTime(float foo)
	{
		startTime = foo + Time.time;
	}
	public enum Type
	{
		green,
		plastic,
		chemicals,
		paper
	}
}