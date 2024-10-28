using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    private GameObject iconoVida;
    [SerializeField]
    private GameObject contenedorIconosVida;

    private List<GameObject> iconosVidas = new List<GameObject>();
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.OnLivesChanged.AddListener(UpdateLives);
            UpdateLives(playerHealth.lives);
        }
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
}