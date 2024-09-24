using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private float vida = 5f;

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
            TerminarJuego("GANASTE");
        }
    }
}