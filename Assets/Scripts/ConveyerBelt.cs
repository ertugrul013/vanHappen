using UnityEngine;

public class ConveyerBelt
{
	public enum Dirs
	{
		Left,
		Right,
		Moving
	}

	public GameObject _ThisGameObject;

	public Dirs CheckTurning()
	{
		var rotation = _ThisGameObject.transform.rotation;
		return rotation.z <= -15f ? Dirs.Left :
			rotation.z >= 15f ? Dirs.Right : Dirs.Moving;
	}
}