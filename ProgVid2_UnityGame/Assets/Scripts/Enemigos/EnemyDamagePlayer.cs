using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagePlayer : MonoBehaviour
{
    [SerializeField] private float damageToPlayer = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IDamageable player = collision.gameObject.GetComponent<IDamageable>();
            if (player != null)
            {
                player.TakeDamage((int)damageToPlayer);
            }
        }
    }
}