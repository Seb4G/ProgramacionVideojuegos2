using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saltar : MonoBehaviour
{

    [Header("Configuracion")]
    [SerializeField] private float fuerzaSalto = 5f;

    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip collisionSFX;

    private bool puedoSaltar = true;
    private bool saltando = false;

    private Rigidbody2D miRigidbody2D;
    private AudioSource miAudioSource;

    private void OnEnable()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();
        miAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && puedoSaltar)
        {
            puedoSaltar = false;

            if (miAudioSource.isPlaying) { return; }
            miAudioSource.PlayOneShot(jumpSFX);
        }
    }

    private void FixedUpdate()
    {
        if (!puedoSaltar && !saltando)
        {
            miRigidbody2D.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            saltando = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        puedoSaltar = true;
        saltando = false;

        if (miAudioSource.isPlaying) { return; }
        miAudioSource.PlayOneShot(collisionSFX);
    }
}