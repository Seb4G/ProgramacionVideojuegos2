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
            if (playerController != null)
            {
                playerController.ModificarVida(puntos); // Aumenta la salud (hasta el máximo de 5)
                playerController.AgregarVida(); // Agrega un corazón adicional (hasta un máximo de 5)
            }
        }
    }
}