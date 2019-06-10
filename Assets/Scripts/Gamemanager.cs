using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    [SerializeField] private TextMeshProUGUI countdownText;

    //Spawning
    private readonly Trash _trash = new Trash();

    //keeps track of all the different objects that can been spawn
    private readonly Dictionary<Guid, GameObject> trashObjects = new Dictionary<Guid, GameObject>();

    //the queue of witch the trash has come in
    private readonly Queue<Trash> TrashQueue = new Queue<Trash>();

    //Player settings
    private int _currentLives = 3;
    private int _score;

    private GameObject[] _obstacleObjects;
    private float _sceneSwitchTime;

    //trash objects to spawn
    private GameObject[] _trashObject;
    private float basetime;

    //obstacle objects to spawn

    [SerializeField] private int chanceToSpawn;

    [SerializeField] private Transform[] spawmLocations = new Transform[3];

    [Tooltip("time in seconds")] public float spawnRate;

    //World settings
    [SerializeField] private GameObject world;
    [SerializeField] [Range(20, 70)] private float worldSpeed;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        StartCoroutine(StartupDelay());
    }

    private void StartGame()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            basetime = spawnRate;
            _trash.GetFirstTime(spawnRate);
            GetTrashObjects();
            GetObstacleObjects();
        }
        else
        {
            Destroy(this);
        }
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            SpawnBasedOnTime();
        }
        else
        {
            WorldSpinning();
            SpawnObstacles();
        }

    }

    public void LifeTracking(GameObject col)
    {
        _currentLives--;
        Destroy(col);
        UIController.instance.SetLifeUI(_currentLives);
        if (_currentLives == 0)
        {
            SceneManager.LoadScene(2);
            _sceneSwitchTime = Time.time;
        }
    }

    private void TrashSpawn(GameObject t) => t.GetComponent<TrashConfig>()._trash.SetTime();

    public void AddTrash(GameObject t)
    {
        var x = t.GetComponent<TrashConfig>()._trash;
        x.SetTime();
        TrashQueue.Enqueue(x);
        _score += x.amountOfScore;
        UIController.instance.setScoreText(_score);
        Destroy(t);
    }

    private void WorldSpinning()
    {
        world.transform.Rotate(0, worldSpeed * Time.deltaTime, 0, Space.Self);
    }


    private void SpawnObstacles()
    {
        spawnRate -= 1 * Time.deltaTime;

        if (spawnRate <= 0)
        {
            var k = Random.Range(0, 200);
            var i = Random.Range(0, spawmLocations.Length);

            if (k < chanceToSpawn)
            {
                var j = Random.Range(0, _trashObject.Length);
                var obs = Instantiate(_trashObject[j], spawmLocations[i].position, Quaternion.identity);
                obs.transform.SetParent(world.transform, true);
                TrashSpawn(obs);
            }
            else if (k > chanceToSpawn)
            {
                var j = Random.Range(0, _obstacleObjects.Length);
                var obs = Instantiate(_obstacleObjects[j], spawmLocations[i].position, Quaternion.identity * Quaternion.Euler(295, 0, 0));
                obs.transform.SetParent(world.transform, true);
            }

#if UNITY_EDITOR
            else
            {
                Debug.LogError("Object index out of range");
                EditorApplication.isPlaying = false;
            }
#endif
            spawnRate += basetime;
        }
    }

    private void GetTrashObjects()
    {
        var allObj = Resources.LoadAll("TrashPrefabs/", typeof(GameObject));
        _trashObject = new GameObject[allObj.Length];

        foreach (GameObject obj in allObj)
        {
            var uid = obj.GetComponent<TrashConfig>().Generateuid();
            trashObjects.Add(uid, obj);
            Debug.Log(uid.ToString());
        }

        for (var i = 0; i < allObj.Length; i++) _trashObject[i] = allObj[i] as GameObject;
    }

    private void GetObstacleObjects()
    {
        //load the assets in
        var allObs = Resources.LoadAll("ObstaclesPrefabs/", typeof(GameObject));

        //SetLength Array
        _obstacleObjects = new GameObject[allObs.Length];

        //assign the objects to the array
        for (var i = 0; i < allObs.Length; i++) _obstacleObjects[i] = allObs[i] as GameObject;
    }

    private void SpawnBasedOnTime()
    {
        //dequeue the next trash object
        var curTrash = TrashQueue.Dequeue();
        //get the right GameObject to spawn
        var curTrashObject = trashObjects[curTrash.GUID];

        if (curTrash.pickUpTime <= Time.time - _sceneSwitchTime)
        {
            GameObject i;
            i = Instantiate(curTrashObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            i.AddComponent<Rigidbody>().useGravity = true;
        }
    }

    IEnumerator StartupDelay()
    {
        Time.timeScale = 0;
        UIController.instance.isPaused = true;
        var timer = 4f;
        float pauseTime = Time.realtimeSinceStartup + timer;
        while (Time.realtimeSinceStartup < pauseTime)
        {
            yield return new WaitForSecondsRealtime(1f);
            timer--;
            countdownText.SetText(timer.ToString());

        }
        Time.timeScale = 1;
        countdownText.SetText("");
        UIController.instance.isPaused = false;
        StartGame();
        yield break;
    }
}