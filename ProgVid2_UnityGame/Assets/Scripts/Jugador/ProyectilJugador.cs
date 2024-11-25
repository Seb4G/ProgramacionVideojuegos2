using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilJugador: MonoBehaviour
{
    [Header("Configuración del Proyectil")]
    public float speed = 10f;
    private Vector2 direccionMovimiento;
    private int damageToEnemy;

    private void Update()
    {
        transform.Translate(direccionMovimiento.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            return;
        }
        if (collision.CompareTag("Enemy"))
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damageToEnemy);
            }
        }

        gameObject.SetActive(false);
    }

    public void ConfigureProjectile(Vector2 direction, int damage)
    {
        direccionMovimiento = direction;
        damageToEnemy = damage;
    }
}