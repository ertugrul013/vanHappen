using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Movement")]
    [SerializeField] private Vector3[] _lanePos = new Vector3[3];
    private Vector3 _target;
    [SerializeField] private Lanes currentLane = Lanes.Middel;
    [SerializeField] private float swipeThreshold;
	
	[Space]
	[Header("audio")]
	[SerializeField] private AudioSource PickupSound;

    private void Start()
    {
        _target = _lanePos[1];
    }

    private void Update()
    {
        //checks if the game is paused else continues
        if (Time.timeScale == 0) return;
        GetSwipe();
        transform.position = Vector3.Lerp(transform.position, _target, 0.25f);
    }

    private void GetSwipe()
    {
#if PLATFORM_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            var deltaPosition = Input.GetTouch(0).deltaPosition;

            if (deltaPosition.x > swipeThreshold)
                LaneSwitch(false);
            else if (deltaPosition.x < swipeThreshold) LaneSwitch(true);
        }
#endif

        if (Input.GetKeyDown(KeyCode.A))
            LaneSwitch(true);
        else if (Input.GetKeyDown(KeyCode.D)) LaneSwitch(false);

    }

    /// <summary>
    ///     Case checks on
    /// </summary>
    /// <param name="isSwippedLeft"> wich way has there been swiped</param>
    private void LaneSwitch(bool isSwippedLeft)
    {
        // 0 = left
        // 1 = middel
        // 2 = right
        switch (currentLane)
        {
            case Lanes.Left:
                if (!isSwippedLeft)
                {
                    _target = _lanePos[1];
                    currentLane = Lanes.Middel;
                }

                break;

            case Lanes.Middel:
                if (isSwippedLeft)
                {
                    _target = _lanePos[0];
                    currentLane = Lanes.Left;
                }
                else if (!isSwippedLeft)
                {
                    _target = _lanePos[2];
                    currentLane = Lanes.Right;
                }

                break;

            case Lanes.Right:
                if (isSwippedLeft)
                {
                    _target = _lanePos[1];
                    currentLane = Lanes.Middel;
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
        //_animator.SetBool(index, false);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("obstacle"))
            Gamemanager.instance.LifeTracking(col.gameObject);
        else if (col.gameObject.CompareTag("pickup"))
        {
            Gamemanager.instance.AddTrash(col.gameObject);
			PickupSound.Play();
		}
    }

    private enum Lanes
    {
        Left,
        Middel,
        Right
    }
}