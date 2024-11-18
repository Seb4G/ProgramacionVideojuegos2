using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirPorColision2D : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisión detectada con: " + collision.gameObject.name);
        Destroy(gameObject);
    }
}