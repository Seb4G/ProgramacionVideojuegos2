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

    void Start()
    {
        tasks = new Stack<string>();

        metaObject = GameObject.FindGameObjectWithTag("Meta");
        if (metaObject != null)
        {
            metaObject.SetActive(false);
        }
        tasks.Push("Task 3: Derrotar al jefe");
        tasks.Push("Task 2: Encontrar la llave");
        tasks.Push("Task 1: Activar el interruptor");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isMetaUnlocked)
            {
                if (metaObject != null)
                {
                    metaObject.SetActive(true);
                    Debug.Log("¡Meta desbloqueada!");
                    isMetaUnlocked = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coleccionable"))
        {
            tasks.Push("Recolectado: " + collision.gameObject.name);
            coleccionablesRecolectados++;

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