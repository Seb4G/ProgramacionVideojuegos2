using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public int currentHealth;
    [SerializeField] private int maxHealth = 5;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemigo recibi� da�o. Salud restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            GetComponent<EnemyDeath>().Die();
        }
    }
}