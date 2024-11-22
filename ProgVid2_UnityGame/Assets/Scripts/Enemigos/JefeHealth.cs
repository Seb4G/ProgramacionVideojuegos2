using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 25;
    [SerializeField] private Animator animator;
    private bool isDead = false;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        Debug.Log("Vida inicial del jefe: " + health);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        Debug.Log("Recibiendo daño: " + damage);
        health -= damage;
        Debug.Log("Vida restante del jefe: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.enabled = true;
        Debug.Log("Jefe ha muerto. Activando animación de muerte.");
        animator.SetTrigger("Death");
        StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            yield return null;
        }
        Debug.Log("Animación de muerte en progreso.");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Debug.Log("Animación de muerte completada. Desactivando el objeto.");
        gameObject.SetActive(false);
        GameEvents.TriggerVictory();
    }
}