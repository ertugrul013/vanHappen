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
        GetBeltPos();
    }

    // Update is called once per frame
    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        /*
        if (Input.GetAxis("Fire1")!=0)
        {   
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.CompareTag("belt"))
                {
                     hit.collider.gameObject.GetComponent<BeltConfig>().SetTarget();
                }
            }

            
        }
        */
    }

    private void GetBeltPos()
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