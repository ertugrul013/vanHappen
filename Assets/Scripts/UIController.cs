using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
	public void LoadScene(int index)
	{
		SceneManager.LoadScene(index);
	}
}