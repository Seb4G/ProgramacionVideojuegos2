using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 5;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemigo recibió daño. Salud restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            GetComponent<EnemyDeath>().Die();
        }
    }
}