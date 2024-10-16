using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el proyectil colisiona con el jefe, ignorar la colisi�n
        if (collision.CompareTag("JefeFinal"))
        {
            return;
        }
        Destroy(gameObject);
    }
}