using System;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class TrashConfig : MonoBehaviour
{
	public Trash _trash = new Trash();

	public Guid GUID;
	public string test;

	public Trash.Type type;

	private bool isGame2;
	private void Awake()
	{
		isGame2 = false;
		
		if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 2) 
			isGame2 = true;
	}

	private void Update()
	{
		this.transform.forward = -Camera.main.transform.forward;		
	}

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