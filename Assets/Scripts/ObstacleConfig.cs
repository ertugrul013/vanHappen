using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleConfig : MonoBehaviour
{

    public Obstacle _obstacle = new Obstacle();

   /// <summary>
   /// Awake is called when the script instance is being loaded.
   /// </summary>
   void Awake()
   {
       transform.position = new Vector3(transform.position.x,transform.position.y - _obstacle.offsetY,transform.position.z);
   }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
