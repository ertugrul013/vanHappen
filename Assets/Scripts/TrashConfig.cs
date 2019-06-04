using System;
using UnityEngine;

public class TrashConfig : MonoBehaviour
{
	public Trash _trash = new Trash();

	public Guid GUID;

	public float debug;

	public bool isGame2;

	private void Update()
	{
		if (!isGame2)
		{
			this.transform.forward = -Camera.main.transform.forward;	
		}
	}
	public Trash.Type type;

	public Guid Generateuid()
	{
		if (GUID != null)
		{
			GUID = Guid.NewGuid();
			_trash.GUID = GUID;

			_trash.mytype = type;
		}

		return GUID;
	}
}