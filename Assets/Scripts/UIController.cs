using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [Header("Life images")]
    [SerializeField] private Image[] LifeUiElements = new Image[3];

    [Space]
    [Header("pause control")]
    [SerializeField] private Image pausePlayButt;
    [SerializeField] private Sprite[] StateSprite = new Sprite[2];
    public bool isPaused;

    [Space]
    [Header("Score control")]
    [SerializeField] private TextMeshProUGUI scoreText;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        isPaused = false;
        instance = this;
    }

    /// <summary>
    /// switches scene to the index corresponding to the build settings
    /// </summary>
    /// <param name="index">build settings corresponding to the index of the scene you wan't to switch to</param>
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    /// <TO-DO>
    /// Check with Elysa(main 2D artist) if the health icons can be reduced to
    /// show 2 health for better understanding that there are only 2 lives. 
    /// <summary>
    /// Sets the UI lives according to the current amount of lives left
    /// </summary>
    /// <param name="amountOfLifes">current amount of lives</param>
    public void SetLifeUI(int amountOfLifes) => LifeUiElements[amountOfLifes].enabled = false;

    /// <summary>
    /// handels the starting and pauzing of the game
    /// </summary>
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

    /// <summary>
    /// Sets the text of the score to the current score
    /// </summary>
    /// <param name="score">score to add</param>
    public void setScoreText(int score) => scoreText.SetText("score:\n {0}", score);

    public void ExitGame() => Application.Quit();
}