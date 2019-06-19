using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    [Header("PowerUp")]
    public PowerUpManager powerUpManager = new PowerUpManager();
    //Spawning
    private Trash _trash = new Trash();

    //keeps track of all the different objects that can been spawn
    private Dictionary<Guid, GameObject> trashObjects = new Dictionary<Guid, GameObject>();

    //the queue of witch the trash has come in
    private Queue<Trash> TrashQueue = new Queue<Trash>();

    [Header("Player settings")]
    public int CurrentLives = 3;
    public int _score;
    public bool firstSwipe;

    private GameObject[] _obstacleObjects;
    private float _sceneSwitchTime;

    //trash objects to spawn
    public GameObject[] _trashObject;
    private float basetime;

    //obstacle objects to spawn

    [Space]
    [Header("Spawn Settings")]
    [SerializeField] private int chanceToSpawn;

    [SerializeField] private Transform[] spawmLocations = new Transform[3];

    [Tooltip("time in seconds")] public float spawnRate;

    //World settings
    public GameObject world;
    [SerializeField] [Range(20, 70)] private float worldSpeed;

    [Space]
    [Header("Tutorial")]
    public float animPlaySpeed;
    [SerializeField] private Sprite[] SwipeTutorial;
    [SerializeField] private Sprite[] TickTutorial;

    [Header("game2")]
    [SerializeField] public Transform spawnpoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            StartCoroutine(StartupDelay());
        }
        else
        {
            Destroy(this);
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        PlayTutorialImages();
    }


    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            if (spawnpoint == null)
            {
                spawnpoint = GameObject.Find("spawnpos").transform;
                basetime = 5f;
                UIController.instance.setScoreText(Gamemanager.instance._score);
                CurrentLives = 3;
                powerUpManager.FactoryTrash = PipeController.instance.trashPile;
            }
            if (CurrentLives <= 0)
            {
                Destroy(this.gameObject);
                SceneManager.LoadScene(3);
            }
            Debug.Log("spawning");
            RandomSpawn();
        }
        else
        {
            WorldSpinning();
            SpawnObstacles();
            worldSpeed += Time.deltaTime / 2;
            //sets a limit of how fast the object can spawn
            if (basetime > 0.5f)
            {
                basetime -= Time.deltaTime / 25;
            }
        }
    }

    void StartGame()
    {
        basetime = spawnRate;
        _trash.GetFirstTime(spawnRate);
        GetTrashObjects();
        GetObstacleObjects();
    }

    public void LifeTracking(GameObject col)
    {
        CurrentLives--;
        Destroy(col);
        UIController.instance.SetLifeUI(CurrentLives);
        if (CurrentLives == 0)
        {
            SceneManager.LoadScene(2);
            _sceneSwitchTime = Time.time;
            firstSwipe = false;
        }
    }

    private void TrashSpawn(GameObject t) => t.GetComponent<TrashConfig>()._trash.SetTime();

    public void AddTrash(GameObject t)
    {
        var x = t.GetComponent<TrashConfig>();
        x._trash.SetTime();
        TrashQueue.Enqueue(x._trash);
        //score setting
        _score += x._trash.amountOfScore;
        UIController.instance.setScoreText(_score);
        //powerUp
        powerUpManager.PowerUpHandeler(x);
        t.SetActive(false);
        Debug.Log(TrashQueue.Count);
    }

    private void WorldSpinning()
    {
        world.transform.Rotate(0, worldSpeed * Time.deltaTime, 0, Space.Self);
    }

    #region objectSpawning

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
                //obs.transform.position = spawmLocations[i].position;
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

    /// <summary>
    /// Spawns the trash objects randomly !only in the second game
    /// </summary>
    private void RandomSpawn()
    {
        spawnRate -= 1 * Time.deltaTime;
        //Debug.Log(spawnRate);
        if (spawnRate <= 0)
        {
            Debug.Log("spawned");
            //spawning of the trash object
            var j = Random.Range(0, _trashObject.Length);
            Debug.Log(j);
            var obs = Instantiate(_trashObject[j], spawnpoint.position, Quaternion.identity);

            //settings adjusment for the needed components
            obs.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
            obs.AddComponent<Rigidbody>().freezeRotation = true;
            obs.GetComponent<BoxCollider>().enabled = false;
            obs.AddComponent<SphereCollider>();

            //reset timer
            spawnRate += basetime;
        }
    }
    #endregion

    private void PlayTutorialImages()
    {
        if (!firstSwipe)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                UIController.instance.tutorialImage.sprite = UIController.instance.playAnimtion(SwipeTutorial, animPlaySpeed);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                Debug.Log("tutoriol playing");
                UIController.instance.tutorialImage.sprite = UIController.instance.playAnimtion(TickTutorial, animPlaySpeed);
            }
        }
    }

    /// <summary>
    /// Delays the startup
    /// </summary>
    /// <returns><see langword="null"/></returns>
    IEnumerator StartupDelay()
    {
        Time.timeScale = 0;
        UIController.instance.isPaused = true;
        var timer = 3f;
        float pauseTime = Time.realtimeSinceStartup + timer;
        while (Time.realtimeSinceStartup < pauseTime)
        {
            yield return new WaitForSecondsRealtime(1f);
            timer--;
            UIController.instance.countdownText.SetText(timer.ToString());
        }
        Time.timeScale = 1;
        UIController.instance.countdownText.SetText("");
        UIController.instance.isPaused = false;
        StartGame();
        yield break;
    }
}