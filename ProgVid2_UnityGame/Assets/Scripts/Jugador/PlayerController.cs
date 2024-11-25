using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private int attackDamage = 5;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] public ObjectPooler objectPooler;
    [SerializeField] private LayerMask enemyLayer; 
    [SerializeField] private AudioClip deathAudioClip;
    [SerializeField] private AudioClip attackAudioClip;
    [SerializeField] private AudioClip jumpAudioClip;
    [SerializeField] private AudioClip damageAudioClip;
    private AudioSource audioSource;
    public GameManager gameManager;
    public PlayerData playerData;
    private Rigidbody2D rb2D;
    private Animator miAnimator;
    private SpriteRenderer miSprite;
    private BoxCollider2D miCollider2D;
    private int saltarMask;

    private bool isHurt = false;
    private bool isDead = false;
    private bool isAttacking = false;
    public float invulnerabilityTime = 1f;
    private PlayerHealth playerHealth;
    private HUDController hudController;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        miAnimator = GetComponent<Animator>();
        miSprite = GetComponent<SpriteRenderer>();
        miCollider2D = GetComponent<BoxCollider2D>();
        saltarMask = LayerMask.GetMask("Pisos", "Plataformas");
        playerHealth = GetComponent<PlayerHealth>();
        hudController = FindObjectOfType<HUDController>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
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
        if (Input.GetMouseButtonDown(0))
        {
            LanzarProyectil();
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

        if (moverHorizontal != 0)
        {
            float fuerzaHorizontal = moverHorizontal * playerData.speed;
            rb2D.AddForce(new Vector2(fuerzaHorizontal, 0f));
            miSprite.flipX = moverHorizontal < 0;
        }
        else
        {
            Vector2 velocidadActual = rb2D.velocity;
            velocidadActual.x *= 0.9f;
            rb2D.velocity = velocidadActual;
        }
        float velocidadMaxima = 20f;
        rb2D.velocity = new Vector2(
            Mathf.Clamp(rb2D.velocity.x, -velocidadMaxima, velocidadMaxima),
            rb2D.velocity.y
        );
    }

    private void Saltar()
    {
        if (isHurt || isDead) return;
        rb2D.AddForce(Vector2.up * playerData.jumpHeight, ForceMode2D.Impulse);
        if (jumpAudioClip != null)
        {
            audioSource.PlayOneShot(jumpAudioClip);
        }
    }

    private bool EnContactoConPlataforma()
    {
        return miCollider2D.IsTouchingLayers(saltarMask);
    }
    private void LanzarProyectil()
    {
        if (isAttacking) return;

        if (objectPooler == null)
        {
            return;
        }

        Vector2 direccionProyectil = miSprite.flipX ? Vector2.left : Vector2.right;

        GameObject proyectil = objectPooler.GetPooledObject();
        if (proyectil != null)
        {
            proyectil.transform.position = projectileSpawnPoint.position;
            proyectil.transform.rotation = projectileSpawnPoint.rotation;
            proyectil.SetActive(true);

            ProyectilJugador proyectilJugadorScript = proyectil.GetComponent<ProyectilJugador>();
            if (proyectilJugadorScript != null)
            {
                proyectilJugadorScript.ConfigureProjectile(direccionProyectil, attackDamage);
            }

            if (attackAudioClip != null)
            {
                audioSource.PlayOneShot(attackAudioClip);
            }

            StartCoroutine(ResetAttackCooldown());
        }
    }
    private IEnumerator ResetAttackCooldown()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
    private void ActualizarAnimacion()
    {
        if (isDead) return;

        float velocidadHorizontal = rb2D.velocity.x;

        float velocidadSuavizada = Mathf.Lerp(0, Mathf.Sign(velocidadHorizontal), Mathf.Abs(velocidadHorizontal));

        int velocidadRedondeada = Mathf.RoundToInt(velocidadSuavizada);

        miAnimator.SetInteger("Velocidad", Mathf.Abs(velocidadRedondeada));

        miAnimator.SetBool("Herido", isHurt);
        miAnimator.SetBool("EnAire", !EnContactoConPlataforma());

        if (isAttacking)
        {
            miAnimator.SetTrigger("Atacar");
            isAttacking = false;
        }
        else
        {
            miAnimator.ResetTrigger("Atacar");
        }
    }

    public void HandleDeath()
    {
        if (isDead) return;
        isDead = true;
        rb2D.velocity = Vector2.zero;
        rb2D.isKinematic = true;
        miCollider2D.enabled = false;
        miAnimator.SetTrigger("Muerto");
        if (deathAudioClip != null)
        {
            audioSource.PlayOneShot(deathAudioClip);
        }
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
    }
    public void ModificarVida(float puntos)
    {
        if (puntos > 0)
        {
            playerHealth.GainLife();
        }
    }
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        playerData.health -= damage;
        if (damageAudioClip != null)
        {
            audioSource.PlayOneShot(damageAudioClip);
        }
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