using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Gamemanager : MonoBehaviour
{
	public static Gamemanager instance;

	//Spawning
	private readonly Trash _trash = new Trash();

	//keeps track of all the different objects that can been spawn
	private readonly Dictionary<Guid, GameObject> trashObjects = new Dictionary<Guid, GameObject>();

	//the queue of witch the trash has come in
	private readonly Queue<Trash> TrashQueue = new Queue<Trash>();

	//Player settings
	private int _currentLives = 3;

	//obstacle objects to spawn
	
	[SerializeField]
	private int chanceToSpawn;
	
	private GameObject[] _obstacleObjects;
	private float _sceneSwitchTime;

	//trash objects to spawn
	private GameObject[] _trashObject;
	private float basetime;

	[SerializeField] private Vector3[] spawmLocations = new Vector3[3];

	[Tooltip("time in seconds")] public float spawnRate;

	//World settings
	[SerializeField] private GameObject world;
	[SerializeField] [Range(20, 70)] private float worldSpeed;


	private void Awake()
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
		WorldSpinning();
		SpawnObstacles();
	}

	public void LifeTracking(GameObject col)
	{
		_currentLives--;
		Destroy(col);
		Debug.Log(_currentLives);
		if (_currentLives != 0) return;
		SceneManager.LoadScene(2);
		_sceneSwitchTime = Time.time;
	}

	private void TrashSpawn(GameObject t)
	{
		t.GetComponent<TrashConfig>()._trash.SetTime();
	}

	public void AddTrash(GameObject t)
	{
		var x = t.GetComponent<TrashConfig>();
		x._trash.SetTime();
		TrashQueue.Enqueue(x._trash);
		Destroy(t);
	}

	private void WorldSpinning()
	{
		world.transform.Rotate(-worldSpeed * Time.deltaTime, 0, 0, Space.Self);
	}


	private void SpawnObstacles()
	{
		spawnRate -= 1 * Time.deltaTime;

		if (!(spawnRate <= 0)) return;
		var k = Random.Range(0, 101);
		var i = Random.Range(0, spawmLocations.Length);

		if (k < chanceToSpawn)
		{
			var j = Random.Range(0, _trashObject.Length);
			var obs = Instantiate(_trashObject[j], spawmLocations[i], Quaternion.identity);
			obs.transform.SetParent(world.transform, true);
			TrashSpawn(obs);
		}
		else if (k > chanceToSpawn)
		{
			var j = Random.Range(0, _obstacleObjects.Length);
			var obs = Instantiate(_obstacleObjects[j], spawmLocations[i], Quaternion.identity);
			obs.transform.SetParent(world.transform, true);
		}
		else
		{
			Debug.LogError("Object index out of range");
			EditorApplication.isPlaying = false;
		}

		spawnRate += basetime;
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
			Instantiate(curTrashObject, new Vector3(0, 0, 0), Quaternion.identity);
	}
}