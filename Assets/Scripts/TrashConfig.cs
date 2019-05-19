using System;
using UnityEngine;

public class TrashConfig : MonoBehaviour
{
	public Trash _trash = new Trash();

	public Guid GUID;
	public string test;

	public Trash.Type type;

	public Guid Generateuid()
	{
		if (GUID != null)
		{
			GUID = Guid.NewGuid();
			test = GUID.ToString();
			_trash.GUID = GUID;

			_trash.mytype = type;
		}

		return GUID;
	}
}