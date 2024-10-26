using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour, IDamageable
{
    public PlayerData playerData;
    private Rigidbody2D rb2D;
    private Animator miAnimator;
    private SpriteRenderer miSprite;
    private BoxCollider2D miCollider2D;
    private int saltarMask;

    private bool isHurt = false;
    private bool isDead = false;
    public float invulnerabilityTime = 1f;
    private PlayerHealth playerHealth;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        miAnimator = GetComponent<Animator>();
        miSprite = GetComponent<SpriteRenderer>();
        miCollider2D = GetComponent<BoxCollider2D>();
        playerData.health = 5;
        saltarMask = LayerMask.GetMask("Pisos", "Plataformas");
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (playerHealth.IsPlayerDead())
        {
            if (!isDead)
            {
                HandleDeath();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && EnContactoConPlataforma())
        {
            Saltar();
        }

        ActualizarAnimacion();
    }
    private void FixedUpdate()
    {
        if (!isDead && !isHurt)
        {
            Mover();
        }
    }

    private void Mover()
    {
        if (isHurt || isDead) return;

        float moverHorizontal = Input.GetAxis("Horizontal");
        rb2D.AddForce(new Vector2(moverHorizontal * playerData.speed, 0f));

        if (moverHorizontal != 0)
        {
            miSprite.flipX = moverHorizontal < 0;
        }
    }

    private void Saltar()
    {
        if (isHurt || isDead) return;
        rb2D.AddForce(Vector2.up * playerData.jumpHeight, ForceMode2D.Impulse);
    }

    private bool EnContactoConPlataforma()
    {
        return miCollider2D.IsTouchingLayers(saltarMask);
    }

    private void ActualizarAnimacion()
    {
        if (isDead)
        {
            return;
        }

        miAnimator.SetInteger("Velocidad", Mathf.Abs((int)rb2D.velocity.x));
        miAnimator.SetBool("Herido", isHurt);
        miAnimator.SetBool("EnAire", !EnContactoConPlataforma());
    }

    public void HandleDeath()
    {
        if (isDead) return;
        isDead = true;
        rb2D.velocity = Vector2.zero;
        rb2D.isKinematic = true;
        miCollider2D.enabled = false;
        miAnimator.SetTrigger("Muerto");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isDead)
            {
                playerHealth.LoseLife();
                if (playerHealth.IsPlayerDead())
                {
                    HandleDeath();
                }
                else
                {
                    StartCoroutine(HurtRoutine());
                }
            }
        }
        else if (collision.gameObject.CompareTag("JefeFinal"))
        {
            GameEvents.TriggerVictory();
        }
    }

    public void ModificarVida(float puntos)
    {
        if (isDead) return;
        playerData.health += (int)puntos;
        Debug.Log("Vida actual: " + playerData.health);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        playerData.health -= damage;
        Debug.Log("Vida actual: " + playerData.health);

        if (playerData.health > 0)
        {
            StartCoroutine(HurtRoutine());
        }
    }

    private IEnumerator HurtRoutine()
    {
        isHurt = true;

        miAnimator.SetBool("Herido", true);

        yield return new WaitForSeconds(0.5f);

        isHurt = false;

        miAnimator.SetBool("Herido", false);

        yield return new WaitForSeconds(invulnerabilityTime);
    }
}