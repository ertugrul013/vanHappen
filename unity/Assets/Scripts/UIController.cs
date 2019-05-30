using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	private Image[] LifeUiElements;
	public void LoadScene(int index)
	{
		SceneManager.LoadScene(index);
	}

	public void SetLifeUI(int amountOfLifes)
	{
		
	}
}