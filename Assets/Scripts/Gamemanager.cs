using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
	public static Gamemanager instance;

	private readonly List<Trash> trash = new List<Trash>();

	private int _currentLifes = 3;
	[SerializeField] private Vector2[] spawmLocations = new Vector2[3];
	public float spawnRate, basetime;
	[SerializeField] private GameObject[] trashObject;

	[SerializeField] private GameObject world;
	[SerializeField] [Range(40, 70)] private float worldSpeed;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this);
			basetime = spawnRate;
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

	public void LifeTracking()
	{
		_currentLifes--;
		if (_currentLifes == 0) SceneManager.LoadScene(4);
	}

	public void TrashOrderTracking(GameObject t)
	{
		var ct = t.GetComponent<Trash>();
		ct.SetTime();
		trash.Add(ct);
	}

	private void WorldSpinning()
	{
		world.transform.Rotate(worldSpeed * Time.deltaTime, 0, 0, Space.Self);
	}


	private void SpawnObstacles()
	{
		spawnRate -= 1 * Time.deltaTime;

		if (spawnRate <= 0)
		{
			var i = Random.Range(0, 3);
			var j = Random.Range(0, 3);
			var obs = Instantiate(trashObject[j], spawmLocations[i], Quaternion.identity);
			obs.transform.SetParent(world.transform, true);
			TrashOrderTracking(obs);
			spawnRate = basetime;
		}
	}