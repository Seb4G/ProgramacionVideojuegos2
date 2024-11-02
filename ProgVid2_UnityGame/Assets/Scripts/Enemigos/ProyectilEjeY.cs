using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilEjeY : MonoBehaviour
{
    public float speed = 10f;
    [SerializeField] private float puntos = 5f;

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("JefeFinal"))
        {
            return;
        }

        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.ModificarVida(-puntos);
        }
        gameObject.SetActive(false);
    }
}