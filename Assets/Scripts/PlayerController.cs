using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("shooting")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletHole;
    public int bulletCount;


    [Header("Movement")]
    [SerializeField] private Vector3[] _lanePos = new Vector3[3];
    private Vector3 _target;
    [SerializeField] private Lanes currentLane;
    [SerializeField] private float swipeThreshold;

    [Space]
    [Header("audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] sounds;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        // sets the _target to the middel lane
        _target = _lanePos[1];
    }

    private void Update()
    {
        //checks if the game is paused else continues
        if (Time.timeScale == 0) return;
        GetInput();
        transform.position = Vector3.Lerp(transform.position, _target, 0.25f);
    }

    private void GetInput()
    {
#if PLATFORM_ANDROID
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                var deltaPosition = Input.GetTouch(0).deltaPosition;

                if (deltaPosition.x > swipeThreshold)
                    LaneSwitch(false);
                else if (deltaPosition.x < swipeThreshold) LaneSwitch(true);
            }
            if (Input.touchCount > 0)
            {
                Shoot();
            }
        }
#endif

        if (Input.GetKeyDown(KeyCode.A))
            LaneSwitch(true);
        else if (Input.GetKeyDown(KeyCode.D)) LaneSwitch(false);
        else if (Input.GetKeyDown(KeyCode.Space)) Shoot();
    }

    /// <summary>
    /// checks wich way the player has moved and sets the correct <see cref="currentLane"/> and <see cref="_target"/> for the movement
    /// </summary>
    /// <param name="isSwippedLeft">if the movement direction is left</param>
    private void LaneSwitch(bool isSwippedLeft)
    {
        //turn off tutorial UI after the first swipe
        Gamemanager.instance.firstSwipe = true;
        UIController.instance.tutorialImage.enabled = false;
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

    private void Shoot()
    {
        if (bulletCount > 0)
        {
            Instantiate(bullet, bulletHole.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// Keeps track if the player has collided with any obstacles or pickup
    /// </summary>
    /// <param name="col">Holds the collision data inside of the scope</param>
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("obstacle"))
        {
            Gamemanager.instance.LifeTracking(col.gameObject);
            var a = col.gameObject.GetComponent<AudioSource>();
            a.Play();
        }
        else if (col.gameObject.CompareTag("pickup"))
        {
            Gamemanager.instance.AddTrash(col.gameObject);
            audioSource.clip = sounds[0];
            audioSource.Play();
        }
    }

    private enum Lanes
    {
        Left,
        Middel,
        Right
    }
}