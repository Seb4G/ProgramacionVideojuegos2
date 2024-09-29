using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private float vida = 5f;

    [Header("Zona Bloqueada")]
    [SerializeField] private GameObject[] bloques;

    private void Start()
    {
        // Solo si quieres llenar el array de bloques al inicio del juego
        bloques = GameObject.FindGameObjectsWithTag("BloqueDestructible");
    }

    public void ModificarVida(float puntos)
    {
        vida += puntos;
        Debug.Log(EstasVivo());
        if (!EstasVivo())
        {
            TerminarJuego("PERDISTE");
        }
    }

    private bool EstasVivo()
    {
        return vida > 0;
    }

    private void TerminarJuego(string mensaje)
    {
        Debug.Log(mensaje);
        Time.timeScale = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Meta"))
        {
            DesbloquearZona();
            Debug.Log("Has llegado a la meta. Zona desbloqueada.");
        }
    }

    private void DesbloquearZona()
    {
        bloques = GameObject.FindGameObjectsWithTag("BloqueDestructible");

        foreach (GameObject bloque in bloques)
        {
            Destroy(bloque);
        }
    }
}