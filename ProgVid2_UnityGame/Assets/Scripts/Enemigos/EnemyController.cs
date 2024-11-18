using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    [Header("Enemy Settings")]
    [SerializeField] private float damageToPlayer = 5f;
    [SerializeField] private float moveDistance = 5f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float restDuration = 2f;
    [SerializeField] private float moveDuration = 3f;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    private bool isMoving = false;
    private Vector2 lastRestPosition;
    private Vector2 targetPosition;
    private float timer;
    private int direction = 1;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        lastRestPosition = transform.position;
        SetNextPosition();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (isMoving && timer >= moveDuration)
        {
            StartRest();
        }
        else if (!isMoving && timer >= restDuration)
        {
            StartMoving();
        }

        if (isMoving)
        {
            MoveEnemy();
        }
    }

    private void StartMoving()
    {
        isMoving = true;
        timer = 0f;
        SetNextPosition();
        animator.SetBool("IsMoving", true);
    }

    private void StartRest()
    {
        isMoving = false;
        timer = 0f;
        animator.SetBool("IsMoving", false);

        direction *= -1;
        lastRestPosition = transform.position;
    }

    private void SetNextPosition()
    {
        targetPosition = lastRestPosition + Vector2.right * moveDistance * direction;
    }

    private void MoveEnemy()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if ((Vector2)transform.position == targetPosition)
        {
            StartRest();
        }
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

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy took damage: " + damage);
    }
}