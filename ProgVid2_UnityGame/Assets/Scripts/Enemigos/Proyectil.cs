using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Configuración del Proyectil")]
    public float damageToPlayer = 1.0f;
    public float speed = 10f;
    [SerializeField] private Vector2 direccionMovimiento = Vector2.left;

    private void Update()
    {
        transform.Translate(direccionMovimiento.normalized * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            return;
        }
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerHealth != null)
            {
                playerHealth.LoseLife();
                playerController.TakeDamage((int)damageToPlayer);
                if (playerHealth.IsPlayerDead())
                {
                    GameEvents.TriggerGameOver();
                }
            }
        }
        gameObject.SetActive(false);
    }
}