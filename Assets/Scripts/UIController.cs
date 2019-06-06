using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	private Image[] LifeUiElements;

	[Header("pause control")]
	[SerializeField] private Image pausePlayButt;
	[SerializeField] private Sprite[] StateSprite = new Sprite[2];
	private bool isPaused;
	
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		isPaused = true;
	}
	
	public void LoadScene(int index)
	{
		SceneManager.LoadScene(index);
	}

	public void SetLifeUI(int amountOfLifes)
	{
		
	}

	public void Pause()
	{
		if (isPaused)
		{
			pausePlayButt.sprite = StateSprite[0];
			Time.timeScale = 0;
		}
		else if (!isPaused)
		{
			pausePlayButt.sprite = StateSprite[1];
			Time.timeScale = 1;
		}
		isPaused = !isPaused;
	}
}