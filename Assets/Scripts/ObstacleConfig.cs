using UnityEngine;

public class ObstacleConfig : MonoBehaviour
{

    public Obstacle _obstacle = new Obstacle();

    [SerializeField] private Sprite[] TrashMonsterAnim;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private AudioSource audioMonster;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - _obstacle.offsetY, transform.position.z);
        if (_obstacle.myType == Obstacle.Type.TrashMonster)
        {
            audioMonster = GetComponent<AudioSource>();
            audioMonster.Play();
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (_obstacle.myType == Obstacle.Type.TrashMonster)
        {
            _spriteRenderer.sprite = UIController.instance.playAnimtion(TrashMonsterAnim, Gamemanager.instance.animPlaySpeed);
            this.transform.forward = -Camera.main.transform.forward;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
