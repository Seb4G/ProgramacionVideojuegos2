using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private GameObject iconoVida;
    [SerializeField] private GameObject contenedorIconosVida;
    [SerializeField] public TextMeshProUGUI textoPuntaje;

    private List<GameObject> iconosVidas = new List<GameObject>();
    private PlayerHealth playerHealth;
    private int puntajeActual = 0;

    private const string HighScoreKey = "HighScore";
    private const string HighScoreNameKey = "HighScoreName";

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.OnLivesChanged.AddListener(UpdateLives);
            UpdateLives(playerHealth.lives);
        }
        PlayerPrefs.GetInt(HighScoreKey, 0);
        UpdateScore(0);
    }

    public void UpdateLives(int lives)
    {
        foreach (GameObject icono in iconosVidas)
        {
            Destroy(icono);
        }
        iconosVidas.Clear();

        for (int i = 0; i < lives; i++)
        {
            GameObject nuevoIcono = Instantiate(iconoVida, contenedorIconosVida.transform);
            iconosVidas.Add(nuevoIcono);
        }
    }
    public void UpdateScore(int puntos)
    {
        puntajeActual += puntos;
        if (textoPuntaje != null)
        {
            textoPuntaje.text = $"Score: {puntajeActual}";
        }
        int highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        if (puntajeActual > highScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, puntajeActual);
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                string playerName = gameManager.GetPlayerName();
                PlayerPrefs.SetString(HighScoreNameKey, playerName);
            }
            PlayerPrefs.Save();
        }
    }
    public void SavePlayerName(string playerName)
    {
        PlayerPrefs.SetString(HighScoreNameKey, playerName);
        PlayerPrefs.Save();
    }
    public int GetCurrentScore()
    {
        return puntajeActual;
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HighScoreKey, 0);
    }
    public string GetHighScoreName()
    {
        return PlayerPrefs.GetString(HighScoreNameKey, "No Name");
    }
    public void ShowHighScore()
    {
        int highScore = GetHighScore();
        string highScoreName = GetHighScoreName();
    }
}