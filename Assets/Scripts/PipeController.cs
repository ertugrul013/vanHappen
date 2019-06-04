using UnityEngine;

public class PipeController : MonoBehaviour
{
    [SerializeField] private int swipeThreshold;

    // 0 = top
    // 1 = bot left
    // 2 = bot right 
    [SerializeField] private BeltConfig[] _beltConfigs = new BeltConfig[3];
    [SerializeField] private TypeOfBelts currentSelectedBelt;

    private Quaternion Pos1, Pos2, Pos1Top, Pos2Top;

    // Start is called before the first frame update
    private void Start()
    {
        currentSelectedBelt = TypeOfBelts.Top;
        SetBeltPos();
    }

    // Update is called once per frame
    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentSelectedBelt == TypeOfBelts.Top)
            {
                _beltConfigs[(int)currentSelectedBelt].target = Pos1Top;
                return;
            }

            _beltConfigs[(int)currentSelectedBelt].target = Pos1;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentSelectedBelt == TypeOfBelts.Top)
            {
                _beltConfigs[(int)currentSelectedBelt].target = Pos2Top;
                return;
            }

            _beltConfigs[(int)currentSelectedBelt].target = Pos2;
        }
#endif

		#if PLATFORM_ANDROID

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            var deltaPosition = Input.GetTouch(0).deltaPosition;

            if (deltaPosition.x > swipeThreshold)
            {
                if (currentSelectedBelt == TypeOfBelts.Top)
                {
                    _beltConfigs[(int)currentSelectedBelt].target = Pos1Top;
                    return;
                }

                _beltConfigs[(int)currentSelectedBelt].target = Pos1;
            }

            else if (deltaPosition.x < swipeThreshold)
            {
                if (currentSelectedBelt == TypeOfBelts.Top)
                {
                    _beltConfigs[(int)currentSelectedBelt].target = Pos2Top;
                    return;
                }

                _beltConfigs[(int)currentSelectedBelt].target = Pos2;
            }
        }
	}
        public void SetBelt(int _enum)
        {
            currentSelectedBelt = (TypeOfBelts)_enum;
        }

        private void SetBeltPos()
        {
            var a = new Vector3(0, 11, 0);
            var b = new Vector3(0, -11, 0);
            var c = new Vector3(0, 20, 0);
            var d = new Vector3(0, -20, 0);
            Pos1 = Quaternion.Euler(a);
            Pos2 = Quaternion.Euler(b);
            Pos1Top = Quaternion.Euler(c);
            Pos2Top = Quaternion.Euler(d);
        }


    private enum TypeOfBelts
    {
        Top,
        BotLeft,
        BotRight
    }
}