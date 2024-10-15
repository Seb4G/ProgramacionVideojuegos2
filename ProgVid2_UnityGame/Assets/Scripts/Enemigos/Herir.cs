using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herir : MonoBehaviour
{
    // Variables a configurar desde el editor
    [Header("Configuracion")]
    [SerializeField] float puntos = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)  // Verifica que se encontró el componente
            {
                playerController.TakeDamage((int)puntos); // Llama a TakeDamage con los puntos de daño
                Debug.Log("PUNTOS DE DAÑO REALIZADOS AL JUGADOR: " + puntos);
            }
        }
    }
}