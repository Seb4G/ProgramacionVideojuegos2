using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageableObject = collision.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}