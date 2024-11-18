using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilEjeY : MonoBehaviour
{
    [SerializeField] private float damageToPlayer = 5f;
    public float speed = 10f;

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }
}