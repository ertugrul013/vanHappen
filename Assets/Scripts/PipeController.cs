using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    [SerializeField] private int swipeThreshold;
    
    private BeltDir _topBeltDir = BeltDir.Left;
    private BeltDir _botLeftBeltDir = BeltDir.Left;
    private BeltDir _botRightBeltDir = BeltDir.Right;
    
    // 0 = top
    // 1 = bot left
    // 2 = bot right 
    [SerializeField] private GameObject[] belts = new GameObject[3];

    [SerializeField] private TypeOfBelts currentSelectedBelt;
    
    private Quaternion Pos1,Pos2;
    public float rotationDegreesPerSecond = 45f;
    public float rotationDegreesAmount = 15f;
    private float totalRotation = 0;
        // Start is called before the first frame update
    void Start()
    {
        currentSelectedBelt = TypeOfBelts.Top;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        SetBeltDir();
    }

    void GetInput()
    {
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.D))
        {
            SetBeltDir();
            Debug.Log("hoi");
        }
#endif
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            var deltaPosition = Input.GetTouch(0).deltaPosition;

            if (deltaPosition.x > swipeThreshold)
                SetBeltDir();
            else if (deltaPosition.x < swipeThreshold) SetBeltDir();
        }
    }

    void SetBeltDir()
    {
        if (currentSelectedBelt == TypeOfBelts.Top)
        {
            
        }
        else if (currentSelectedBelt == TypeOfBelts.BotLeft)
        {
        }
        else if (currentSelectedBelt == TypeOfBelts.BotRight)
        {
        }
    }
    

    public void SetBelt(int _enum)
    {
        currentSelectedBelt = (TypeOfBelts)_enum;
        Debug.Log(currentSelectedBelt);
    }

    void SetBeltPos()
    {
        var a = new Vector3(0,0,-15);
        var b = new Vector3(0,0,15);
        
        Pos1 = Quaternion.Euler(a);
        Pos2 = Quaternion.Euler(b);
    }

    void SetBeltRotation(Quaternion target)
    { 
        Debug.Log("here");
        belts[0].transform.rotation = Quaternion.Lerp(belts[0].transform.rotation,target,Time.deltaTime * 1);
        belts[1].transform.rotation = Quaternion.Lerp(belts[1].transform.rotation,target,Time.deltaTime * 1);
        belts[2].transform.rotation = Quaternion.Lerp(belts[1].transform.rotation,target,Time.deltaTime * 1);
    }

    private enum BeltDir
    {
        Left,
        Right
    }

    public enum TypeOfBelts
    {
        Top,
        BotLeft,
        BotRight
    }
}
