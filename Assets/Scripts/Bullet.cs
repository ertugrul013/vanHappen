using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private int speed;
    private void Update()
    {
        this.transform.RotateAround(point: Gamemanager.instance.world.transform.position,
                                    axis: Vector3.left, angle: speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.transform.CompareTag("obstacle"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);

        }
        else if (other.gameObject.transform.CompareTag("pickup") || other.gameObject.transform.CompareTag("wall"))
        {
            Destroy(this.gameObject);
        }
        
    }
}