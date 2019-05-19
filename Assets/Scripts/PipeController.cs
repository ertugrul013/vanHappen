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
        switch (currentSelectedBelt)
        {
            case TypeOfBelts.Top:
                if (_topBeltDir == BeltDir.Left)
                {
                    belts[0].transform.lo
                }

                break;
            case TypeOfBelts.BotLeft:
                switch (_botLeftBeltDir)
                {
                    case BeltDir.Left:
                        break;
                    case BeltDir.Right:
                        break;
                }

                break;
            case TypeOfBelts.BotRight:
                switch (_botRightBeltDir)
                {
                    case BeltDir.Left:
                        break;
                    case BeltDir.Right:
                        break;
                }
                break;
            default:
                break;
        }
 
    }

    public void SetBelt(int _enum)
    {
        currentSelectedBelt = (TypeOfBelts)_enum;
        Debug.Log(currentSelectedBelt);
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
