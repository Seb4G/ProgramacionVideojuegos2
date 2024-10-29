using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeFinal : MonoBehaviour, IDamageable
{
    [Header("Configuración")]
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private Animator animator;
    [SerializeField] float tiempoEntreDisparos;
    [SerializeField] float tiempoEntreEmbestidas;
    [SerializeField] float tiempoEntreMovimientos;

    [SerializeField] GameObject prefabProyectil;
    [SerializeField] Transform puntoSpawnProyectil;

    private float tiempoActualEspera;
    private int estadoActual;

    private const int DispararProyectil = 0;
    private const int Embestir = 1;
    private const int Mover = 2;

    void Start()
    {
        estadoActual = DispararProyectil;
        StartCoroutine(ComportamientoJefe());
        currentHealth = maxHealth;
    }

    private IEnumerator ComportamientoJefe()
    {
        while (true)
        {
            switch (estadoActual)
            {
                case DispararProyectil:
                    StartCoroutine(Disparar());
                    tiempoActualEspera = tiempoEntreDisparos;
                    break;
                case Embestir:
                    StartCoroutine(Embestida());
                    tiempoActualEspera = tiempoEntreEmbestidas;
                    break;
                case Mover:
                    StartCoroutine(Movimiento());
                    tiempoActualEspera = tiempoEntreMovimientos;
                    break;
            }
            yield return new WaitForSeconds(tiempoActualEspera);
            ActualizarEstado();
        }
    }

    private IEnumerator Disparar()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(prefabProyectil, puntoSpawnProyectil.position, Quaternion.identity);
        }
    }

    private IEnumerator Embestida()
    {
        float tiempoEmbestida = 2f;
        float tiempoInicio = Time.time;
        float velocidadEmbestida = -10f;

        Vector2 posicionInicial = transform.position;
        Vector2 posicionObjetivo = new Vector2(transform.position.x + velocidadEmbestida, transform.position.y);

        while (Time.time < tiempoInicio + tiempoEmbestida / 2)
        {
            transform.position = Vector2.Lerp(posicionInicial, posicionObjetivo, (Time.time - tiempoInicio) / (tiempoEmbestida / 2));
            yield return null;
        }
        tiempoInicio = Time.time;
        while (Time.time < tiempoInicio + tiempoEmbestida / 2)
        {
            transform.position = Vector2.Lerp(posicionObjetivo, posicionInicial, (Time.time - tiempoInicio) / (tiempoEmbestida / 2));
            yield return null;
        }
    }

    private IEnumerator Movimiento()
    {
        float tiempoMovimiento = 3f;
        float tiempoInicio = Time.time;
        float velocidadMovimiento = 6f;

        Vector2 posicionInicial = transform.position;
        Vector2 posicionObjetivo = new Vector2(transform.position.x, transform.position.y + velocidadMovimiento);

        while (Time.time < tiempoInicio + tiempoMovimiento / 2)
        {
            transform.position = Vector2.Lerp(posicionInicial, posicionObjetivo, (Time.time - tiempoInicio) / (tiempoMovimiento / 2));
            yield return null;
        }

        tiempoInicio = Time.time;
        while (Time.time < tiempoInicio + tiempoMovimiento / 2)
        {
            transform.position = Vector2.Lerp(posicionObjetivo, posicionInicial, (Time.time - tiempoInicio) / (tiempoMovimiento / 2));
            yield return null;
        }
    }

    private void ActualizarEstado()
    {
        estadoActual = Random.Range(0, 3);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("Death");
        Destroy(gameObject, 5.0f);
    }
}
}