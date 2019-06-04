using System;
using UnityEngine;

public class BeltConfig : MonoBehaviour
{
	private static readonly float Speed = 1;
	private readonly ConveyerBelt _conveyerBelt = new ConveyerBelt();
	public Quaternion target;
	public Transform endPoint;
	public void Start()
	{
		_conveyerBelt._ThisGameObject = gameObject;
	}

	private void Update()
	{
		transform.rotation = Quaternion.RotateTowards(transform.rotation, target, Time.deltaTime * 50);
	}

	private void OnTriggerStay(Collider other)
	{
		other.transform.position =
			Vector3.MoveTowards(other.transform.position, endPoint.position, (Speed  * Time.deltaTime)/2);
	}
}