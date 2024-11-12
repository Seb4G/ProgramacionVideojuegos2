using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionSystem : MonoBehaviour
{
    private Stack<string> tasks;
    private bool isMetaUnlocked = false;
    private GameObject metaObject;
    private int coleccionablesRecolectados = 0;
    public int totalColeccionables = 4;

    [SerializeField] private AudioClip recoleccionAudioClip;
    private AudioSource audioSource;
    public GameObject sparkEffectPrefab;

    void Start()
    {
        tasks = new Stack<string>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        metaObject = GameObject.FindGameObjectWithTag("Meta");
        if (metaObject != null)
        {
            metaObject.SetActive(false);
        }
        tasks.Push("Task 3: Derrotar al jefe");
        tasks.Push("Task 2: Encontrar la llave");
        tasks.Push("Task 1: Activar el interruptor");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coleccionable"))
        {
            tasks.Push("Recolectado: " + collision.gameObject.name);
            coleccionablesRecolectados++;

            if (recoleccionAudioClip != null)
            {
                audioSource.PlayOneShot(recoleccionAudioClip);
            }
            if (sparkEffectPrefab != null)
            {
                Instantiate(sparkEffectPrefab, collision.transform.position, Quaternion.identity);
            }
            Destroy(collision.gameObject);

            if (coleccionablesRecolectados >= totalColeccionables && !isMetaUnlocked)
            {
                if (metaObject != null)
                {
                    metaObject.SetActive(true);
                    isMetaUnlocked = true;
                }
            }
        }
        if (collision.CompareTag("Meta"))
        {
            GameObject[] BloquesDestructibles = GameObject.FindGameObjectsWithTag("BloqueDestructible");
            foreach (GameObject bloque in BloquesDestructibles)
            {
                Destroy(bloque);
            }
        }
    }
}