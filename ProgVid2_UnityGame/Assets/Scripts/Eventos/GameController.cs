using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject victoryMenu;
    [SerializeField] private GameObject TextoHerramientas;
    [SerializeField] private GameObject menuControles;
    [SerializeField] private GameObject TextoTornillo;
    [SerializeField] private GameObject TextoJefe;
    [SerializeField] private GameObject textoNombre;
    [SerializeField] private TMP_InputField inputNombre;
    [SerializeField] private GameObject confirmButton;

    private bool isPaused = false;
    private bool isMenuControlesActive = false;
    private bool canShowMenuControles = false;
    private const string HighScoreNameKey = "HighScoreName";

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
        StartCoroutine(EnableNameInputAfterDelay());
        if (TextoHerramientas != null)
        {
            TextoHerramientas.SetActive(false);
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
        Invoke(nameof(EnableMenuControles), 7f);
    }
    private IEnumerator EnableNameInputAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        if (textoNombre != null)
        {
            textoNombre.SetActive(true);
        }

        if (inputNombre != null)
        {
            inputNombre.gameObject.SetActive(true);
        }
        if (confirmButton != null)
        {
            confirmButton.SetActive(true);
        }

        var playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }
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
    public void ConfirmarNombre()
    {
        if (inputNombre != null && !string.IsNullOrEmpty(inputNombre.text))
        {
            string playerName = inputNombre.text;
            PlayerPrefs.SetString(HighScoreNameKey, playerName);
            PlayerPrefs.Save();

            var playerController = FindObjectOfType<PlayerController>();
            if (playerController != null)
            {
                playerController.enabled = true;
            }

            if (textoNombre != null)
            {
                textoNombre.SetActive(false);
            }

            if (inputNombre != null)
            {
                inputNombre.gameObject.SetActive(false);
            }

            GameObject confirmButton = GameObject.Find("ConfirmNameButton");
            if (confirmButton != null)
            {
                confirmButton.SetActive(false);
            }
            if (TextoHerramientas != null)
            {
                TextoHerramientas.SetActive(true);
                Invoke(nameof(HideTextoHerramientas), 5f);
            }
        }
    }
    public string GetPlayerName()
    {
        return PlayerPrefs.GetString(HighScoreNameKey, "No Name");
    }
    private void ShowTextoHerramientas()
    {
        if (AnyOtherMenuActive()) return;
        TextoHerramientas.SetActive(true);
        Invoke(nameof(HideTextoHerramientas), 5f);
    }

    private void HideTextoHerramientas()
    {
        if (TextoHerramientas != null)
        {
            TextoHerramientas.SetActive(false);
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

        var hudController = FindObjectOfType<HUDController>();
        if (hudController != null)
        {
            int currentScore = hudController.GetCurrentScore();
            int highScore = hudController.GetHighScore();
            string highScoreName = hudController.GetHighScoreName();

            TextMeshProUGUI[] texts = victoryMenu.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var text in texts)
            {
                if (text.name == "HighScoreText")
                {
                    text.text = $"High Score: {highScore} by {highScoreName}";
                }
                else if (text.name == "CurrentScoreText")
                {
                    text.text = $"Your Score: {currentScore}";
                }
            }
        }

        if (hudController != null && hudController.textoPuntaje != null)
        {
            hudController.textoPuntaje.gameObject.SetActive(false);
        }

        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
        }
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

    private void EnableMenuControles()
    {
        canShowMenuControles = true;
    }

    private void ToggleMenuControles()
    {
        if (pauseMenu.activeSelf || gameOverMenu.activeSelf || victoryMenu.activeSelf) return;
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
        if (textoNombre != null)
        {
            textoNombre.SetActive(false);
        }
        if (inputNombre != null)
        {
            inputNombre.gameObject.SetActive(false);
        }

        GameObject confirmButton = GameObject.Find("ConfirmNameButton");
        if (confirmButton != null)
        {
            confirmButton.SetActive(true);
        }

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