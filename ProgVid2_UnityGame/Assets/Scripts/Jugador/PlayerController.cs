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
        float moverHorizontal = Input.GetAxis("Horizontal");

        rb2D.AddForce(new Vector2(moverHorizontal * playerData.speed, 0f));

        if (moverHorizontal != 0)
        {
            miSprite.flipX = moverHorizontal < 0;
        }
    }

    private void Saltar()
    {
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
        playerData.health -= damage;
        Debug.Log("Vida actual: " + playerData.health);

        if (playerData.health <= 0)
        {
            Debug.Log("Player has died");
        }
    }
    public void ModificarVida(float puntos)
    {
        playerData.health += (int)puntos;
        Debug.Log("Vida actual: " + playerData.health);
    }
}