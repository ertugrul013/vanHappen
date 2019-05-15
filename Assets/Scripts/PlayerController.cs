using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Animator _animator;
	[SerializeField] private Vector3[] _lanePos = new Vector3[3];
	private Vector3 _target;
	[SerializeField] private Lanes currentlane = Lanes.Middel;
	[SerializeField] private float swipeThreshold;

	private void Start()
	{
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		GetSwipe();
		transform.position = Vector3.Lerp(transform.position, _target, 0.25f);
	}

	private void GetSwipe()
	{
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			var deltaPosition = Input.GetTouch(0).deltaPosition;

			if (deltaPosition.x > swipeThreshold)
				LaneSwitch(false);
			else if (deltaPosition.x < swipeThreshold) LaneSwitch(true);
		}
	}

	/// <summary>
	///     Case checks on
	/// </summary>
	/// <param name="isSwippedLeft"> wich way has there been swiped</param>
	private void LaneSwitch(bool isSwippedLeft)
	{
		var curPos = transform.position;

		// 0 = left
		// 1 = middel
		// 2 = right
		switch (currentlane)
		{
			case Lanes.Left:
				if (!isSwippedLeft)
				{
					_target = _lanePos[1];
					currentlane = Lanes.Middel;
				}

				break;

			case Lanes.Middel:
				if (isSwippedLeft)
				{
					_target = _lanePos[0];
					currentlane = Lanes.Left;
				}
				else if (!isSwippedLeft)
				{
					_target = _lanePos[2];
					currentlane = Lanes.Right;
				}

				break;

			case Lanes.Right:
				if (isSwippedLeft)
				{
					_target = _lanePos[1];
					currentlane = Lanes.Middel;
				}

				break;
		}
	}

	/// <summary>
	///     set the bool of the passed index to false this is used after the anim is played
	/// </summary>
	/// <param name="index">Name of the bool to set to false</param>
	public void AnimExit(string index)
	{
		_animator.SetBool(index, false);
	}

	private void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.CompareTag("obstacle"))
			Gamemanager.instance.LifeTracking();
		else if (col.gameObject.CompareTag("pickup")) Gamemanager.instance.TrashOrderTracking(col.gameObject);
	}

	private enum Lanes
	{
		Left,
		Middel,
		Right
	}
}