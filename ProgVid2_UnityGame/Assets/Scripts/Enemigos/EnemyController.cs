using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    [Header("Configuración")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private Animator animator;
    [SerializeField] private float damageToPlayer = 5f;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemigo recibió daño. Salud restante: " + currentHealth); // Agregar este Debug

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        animator.SetTrigger("Death");
        Destroy(gameObject, 3.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage((int)damageToPlayer);
            }
        }
    }
}