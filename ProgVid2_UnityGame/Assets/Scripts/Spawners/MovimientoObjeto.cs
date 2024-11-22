using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoObjeto : MonoBehaviour
{
    private Vector2 direccion;
    private float velocidad;

    public void ConfigurarMovimiento(Vector2 nuevaDireccion, float nuevaVelocidad)
    {
        direccion = nuevaDireccion.normalized;
        velocidad = nuevaVelocidad;
    }

    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime);
    }
}