using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public float speed;

    protected abstract void Move();

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public class GroundEnemy : Enemy
    {
        private void Update()
        {
            Move();
        }

        protected override void Move()
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
}
