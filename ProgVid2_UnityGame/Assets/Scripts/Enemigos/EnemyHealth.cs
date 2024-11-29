using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public int currentHealth;
    [SerializeField] private int maxHealth = 5;
    [Header("Audio")]
    [SerializeField] private AudioClip damageSound;
    private AudioSource audioSource;
    private void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = 0.1f;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
        if (currentHealth <= 0)
        {
            GetComponent<EnemyDeath>().Die();
        }
    }
}