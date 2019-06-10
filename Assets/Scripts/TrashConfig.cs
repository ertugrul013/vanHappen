using System;
using UnityEngine;

public class TrashConfig : MonoBehaviour
{
    public Trash _trash = new Trash();

    public Guid GUID;

    public bool isGame2;

    private void Update()
    {
        if (!isGame2)
        {
            //billboard functions
            this.transform.forward = -Camera.main.transform.forward;
        }
    }

    public Guid Generateuid()
    {
        if (GUID != null)
        {
            GUID = Guid.NewGuid();
            _trash.GUID = GUID;
        }

        return GUID;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            Destroy(this.gameObject);
        }
    }
}