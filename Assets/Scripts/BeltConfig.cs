using System;
using UnityEngine;

public class BeltConfig : MonoBehaviour
{
    private const float Speed = 1.5f;
    private const float LaneSpeed = 100f;
    public Vector3[] targetsVect = new Vector3[2];
    private Quaternion[] targetsQuat = new Quaternion[2];
    private Quaternion currentTarget;
    public bool isRight;
    public Transform endPoint;

    public AudioSource sound;

    private void Awake()
    {
        targetsQuat[0] = Quaternion.Euler(targetsVect[0]);
        targetsQuat[1] = Quaternion.Euler(targetsVect[1]);
    }

    private void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, currentTarget, Time.deltaTime * LaneSpeed);
    }

    public void SetTarget()
    {
        if (isRight)
        {
            currentTarget = targetsQuat[1];
        }
        else if (!isRight)
        {
            currentTarget = targetsQuat[0];
        }

        isRight = !isRight;
    }

    private void OnTriggerStay(Collider other)
    {
        other.transform.position =
            Vector3.MoveTowards(other.transform.position, endPoint.position, (Speed * Time.deltaTime) / 2);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetTarget();
            sound.Play();
        }
    }

}