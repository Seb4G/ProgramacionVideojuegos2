using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{

    [SerializeField] public int points = 5;
    [SerializeField] private Animator animator;

    public void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }
        Destroy(gameObject, 1.5f);
        HUDController hudController = FindObjectOfType<HUDController>();
        if (hudController != null)
        {
            hudController.UpdateScore(points);
        }
    }
}