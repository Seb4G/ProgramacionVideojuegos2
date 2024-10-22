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

    // Variables para controlar el daño
    private bool isHurt = false;
    private bool isInvulnerable = false; // Para evitar recibir daño repetidamente
    public float invulnerabilityTime = 1f; // Tiempo de invulnerabilidad después de ser herido

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        miAnimator = GetComponent<Animator>();
        miSprite = GetComponent<SpriteRenderer>();
        miCollider2D = GetComponent<BoxCollider2D>();
        playerData.health = 100;
        saltarMask = LayerMask.GetMask("Pisos", "Plataformas");
    }

    private void Update()
    {
        Mover();

        if (Input.GetKeyDown(KeyCode.Space) && EnContactoConPlataforma())
        {
            Saltar();
        }

        ActualizarAnimacion();
    }

    private void Mover()
    {
        if (isHurt) return; // Si el jugador está herido, no puede moverse
        float moverHorizontal = Input.GetAxis("Horizontal");

        rb2D.AddForce(new Vector2(moverHorizontal * playerData.speed, 0f));

        if (moverHorizontal != 0)
        {
            miSprite.flipX = moverHorizontal < 0;
        }
    }

    private void Saltar()
    {
        if (isHurt) return; // Si el jugador está herido, no puede saltar
        rb2D.AddForce(Vector2.up * playerData.jumpHeight, ForceMode2D.Impulse);
    }

    private bool EnContactoConPlataforma()
    {
        return miCollider2D.IsTouchingLayers(saltarMask);
    }

    private void ActualizarAnimacion()
    {
        miAnimator.SetInteger("Velocidad", Mathf.Abs((int)rb2D.velocity.x));

        miAnimator.SetBool("EnAire", !EnContactoConPlataforma());
    }
    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return; // No recibir daño si es invulnerable
        playerData.health -= damage;
        Debug.Log("Vida actual: " + playerData.health);

        if (playerData.health <= 0)
        {
            Debug.Log("Player has died");
        }
    }
    private IEnumerator HurtRoutine()
    {
        isHurt = true;
        isInvulnerable = true;

        // Activar la animación de ser herido
        miAnimator.SetTrigger("Herido");

        // Evitar movimiento mientras está herido
        yield return new WaitUntil(() => miAnimator.GetCurrentAnimatorStateInfo(0).IsName("Herido"));

        // Volver a estado normal y permitir movimiento
        isHurt = false;

        // Tiempo de invulnerabilidad después de ser herido
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }

    private void Die()
    {
        // Activar la animación o lógica de muerte
        Debug.Log("Player has died");
        // Aquí podrías poner un Game Over o reiniciar el nivel
    }

    public void ModificarVida(float puntos)
    {
        playerData.health += (int)puntos;
        Debug.Log("Vida actual: " + playerData.health);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<GameManager>().LoseLife();
        }
        else if (collision.gameObject.CompareTag("Victory"))
        {
            GameEvents.TriggerVictory();
        }
    }
}