using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curar : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] float puntos = 1f;

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null) // Verifica que se encontr� el componente
            {
                playerController.ModificarVida(puntos); // Llama a un nuevo m�todo en PlayerController
                Debug.Log("PUNTOS DE CURACI�N REALIZADOS AL JUGADOR: " + puntos);
            }
        }
    }
}