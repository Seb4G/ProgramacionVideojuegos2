using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }
        Destroy(gameObject, 3.0f);
    }
}