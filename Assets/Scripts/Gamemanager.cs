using System;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;
    
    [SerializeField] private GameObject world;
    [SerializeField][Range(40,70)] private float worldSpeed;
  
    private int currentLifes = 3;
    
    private List<Trash> trash = new List<Trash>();
    [SerializeField] private GameObject _trashObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void FixedUpdate()
    {
        WorldSpinning();
    }

    public void LifeTracking()
    {
        
    }
    
    public void TrashOrderTracking(GameObject t)
    {
        var ct = t.GetComponent<Trash>();
        ct.SetTime();
        trash.Add(ct);
        Destroy(t);
    }

    void WorldSpinning()
    {
        world.transform.Rotate(worldSpeed * Time.deltaTime,0,0,Space.Self);
    }
    
   
    void SpawnObstacles()
    {

    }
}
  