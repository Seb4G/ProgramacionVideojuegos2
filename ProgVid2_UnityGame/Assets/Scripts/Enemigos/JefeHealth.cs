using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour, IDamageable
{
    [SerializeField] public int points = 50;
    [SerializeField] private int health = 25;
    [SerializeField] private Animator animator;
    private bool isDead = false;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        animator.enabled = true;
        animator.SetTrigger("Death");
        StartCoroutine(HandleDeath());
        HUDController hudController = FindObjectOfType<HUDController>();
        if (hudController != null)
        {
            hudController.UpdateScore(points);
        }
    }

    private IEnumerator HandleDeath()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            yield return null;
        }

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
        GameEvents.TriggerVictory();
    }
}