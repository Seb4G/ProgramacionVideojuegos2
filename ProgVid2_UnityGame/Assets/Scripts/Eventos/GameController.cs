using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int lives = 3;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject victoryMenu;

    private bool isPaused = false;

    private void OnEnable()
    {
        GameEvents.OnGameOver += ShowGameOverMenu;
        GameEvents.OnVictory += ShowVictoryMenu;
        GameEvents.OnPause += Pause;
        GameEvents.OnResume += Resume;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= ShowGameOverMenu;
        GameEvents.OnVictory -= ShowVictoryMenu;
        GameEvents.OnPause -= Pause;
        GameEvents.OnResume -= Resume;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (victoryMenu.activeSelf)
            {
                Application.Quit();
            }
            else if (isPaused)
            {
                GameEvents.TriggerResume();
            }
            else
            {
                GameEvents.TriggerPause();
            }
        }
    }

    public void LoseLife()
    {
        lives--;

        if (lives <= 0)
        {
            GameEvents.TriggerGameOver();
            ShowGameOverMenu();
        }
    }

    private void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    private void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    private void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        Invoke("RestartScene", 10f);
    }

    private void ShowVictoryMenu()
    {
        victoryMenu.SetActive(true);
        Invoke("LoadNextScene", 10f);
    }

    private void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNextScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResumeGame()
    {
        GameEvents.TriggerResume();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}