using System;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private AnimatorController _animatorController;
    [SerializeField] private float swipeThreshold;
    
    private void GetSwipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 deltaPosition = Input.GetTouch(0).deltaPosition;

            if (deltaPosition.x > swipeThreshold)
            {
                //move to the right
            }
            else if (deltaPosition.x < swipeThreshold)
            {
                
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("obstacle"))
        {
            
        }
        else if (col.gameObject.CompareTag("pickup"))
        {
            Gamemanager.instance.TrashOrderTracking(col.gameObject);
        }
    }
}
