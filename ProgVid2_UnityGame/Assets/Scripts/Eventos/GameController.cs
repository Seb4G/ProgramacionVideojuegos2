using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject victoryMenu;
    [SerializeField] private GameObject TextoHerramientas;
    [SerializeField] private GameObject menuControles;
    [SerializeField] private GameObject TextoTornillo;
    [SerializeField] private GameObject TextoJefe;

    private bool isPaused = false;
    private bool isMenuControlesActive = false;
    private bool canShowMenuControles = false;

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
    private void Start()
    {
        if (TextoHerramientas != null)
        {
            Invoke(nameof(ShowTextoHerramientas), 3f);
        }
        if (TextoTornillo != null)
        {
            TextoTornillo.SetActive(false);
        }
        if (TextoJefe != null)
        {
            TextoJefe.SetActive(false);
        }
        if (menuControles != null)
        {
            menuControles.SetActive(false);
        }
        Invoke(nameof(EnableMenuControles), 9f);
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
        if (Input.GetKeyDown(KeyCode.H) && canShowMenuControles)
        {
            ToggleMenuControles();
        }
    }

    private void Pause()
    {
        if (AnyOtherMenuActive()) return;
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

        var playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
        }

        Invoke(nameof(QuitGame), 50f);
    }

    public void ShowTextoTornillo()
    {
        if (AnyOtherMenuActive()) return;
        TextoTornillo.SetActive(true);
        Invoke(nameof(HideTextoTornillo), 7f);
    }

    private void HideTextoTornillo()
    {
        if (TextoTornillo != null)
        {
            TextoTornillo.SetActive(false);
        }
    }

    public void ShowTextoJefe()
    {
        if (AnyOtherMenuActive()) return;
        TextoJefe.SetActive(true);
        Invoke(nameof(HideTextoJefe), 7f);
    }

    private void HideTextoJefe()
    {
        if (TextoJefe != null)
        {
            TextoJefe.SetActive(false);
        }
    }

    private void ShowTextoHerramientas()
    {
        if (AnyOtherMenuActive()) return;
        TextoHerramientas.SetActive(true);
        Invoke(nameof(HideTextoHerramientas), 5f);
    }
    private void HideTextoHerramientas()
    {
        TextoHerramientas.SetActive(false);
    }
    private void EnableMenuControles()
    {
        canShowMenuControles = true;
    }

    private void ToggleMenuControles()
    {
        if (pauseMenu.activeSelf || AnyOtherMenuActive() && !isMenuControlesActive) return;
        isMenuControlesActive = !isMenuControlesActive;

        if (isMenuControlesActive)
        {
            Time.timeScale = 0;
            menuControles.SetActive(true);
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1;
            menuControles.SetActive(false);
            isPaused = false;
        }
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
    private bool AnyOtherMenuActive()
    {
        return gameOverMenu.activeSelf || victoryMenu.activeSelf ||
               TextoHerramientas.activeSelf || TextoTornillo.activeSelf ||
               TextoJefe.activeSelf || menuControles.activeSelf;
    }
}