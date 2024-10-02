using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorDiaNoche : MonoBehaviour
{
    [SerializeField] private Camera camara;
    [SerializeField] private Color nuevoColor;
    [SerializeField] private float segundos;

    private float tiempoRestante;

    void Start()
    {
        tiempoRestante = segundos;
    }

    void Update()
    {
        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0)
        {
            camara.backgroundColor = nuevoColor;
            enabled = false; // Desactiva el script para que no siga actualizando
        }
    }
}