using System;
using UnityEngine;

public class Trash
{
	public enum Type
	{
		green,
		plastic,
		chemicals,
		paper
	}

	private static float startTime;
	public Guid GUID;
	public Type mytype;
	public float pickUpTime;

	public void SetTime()
	{
		pickUpTime = Time.time - startTime;
	}

	public void GetFirstTime(float foo)
	{
		startTime = foo + Time.time;
	}
}