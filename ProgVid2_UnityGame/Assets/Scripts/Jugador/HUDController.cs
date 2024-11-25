using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private GameObject iconoVida;
    [SerializeField] private GameObject contenedorIconosVida;
    [SerializeField] private TextMeshProUGUI textoPuntaje;

    private List<GameObject> iconosVidas = new List<GameObject>();
    private PlayerHealth playerHealth;
    private int puntajeActual = 0;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.OnLivesChanged.AddListener(UpdateLives);
            UpdateLives(playerHealth.lives);
        }
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
        textoPuntaje.text = $"Score: {puntajeActual}";
    }
}