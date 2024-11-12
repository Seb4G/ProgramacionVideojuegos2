using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoIA : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] float velocidad = 5f;

    [SerializeField] Transform jugador;

    private Rigidbody2D miRigidbody2D;
    private Vector2 direccion;

    private void Awake()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        direccion = (jugador.position - transform.position).normalized;
        miRigidbody2D.MovePosition(miRigidbody2D.position + direccion * (velocidad * Time.fixedDeltaTime));
    }

}