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

        // Encontrar el objeto con el tag "Meta" y asegurarse de que esté oculto al inicio
        metaObject = GameObject.FindGameObjectWithTag("Meta");
        if (metaObject != null)
        {
            metaObject.SetActive(false);
        }

        // Añadir algunas tareas iniciales
        tasks.Push("Task 3: Derrotar al jefe");
        tasks.Push("Task 2: Encontrar la llave");
        tasks.Push("Task 1: Activar el interruptor");
    }

    void Update()
    {
        // Simulación de completar tareas usando la tecla espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (tasks.Count > 0)
            {
                Debug.Log("Completado: " + tasks.Pop());
            }
            else if (!isMetaUnlocked)
            {
                if (metaObject != null)
                {
                    metaObject.SetActive(true); // Desbloquear la meta
                    Debug.Log("¡Meta desbloqueada!");
                    isMetaUnlocked = true;
                }
            }
            else
            {
                Debug.Log("¡Todas las tareas completadas!");
            }
        }
    }

    // Método que se ejecuta cuando el jugador (quien tiene este script) entra en contacto con un coleccionable
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D detectado con: " + collision.gameObject.name); // Mensaje para depuración
        // Verifica si el objeto con el que colisiona tiene el tag "Coleccionable"
        if (collision.gameObject.CompareTag("Coleccionable"))
        {
            // Añadir el coleccionable como tarea completada
            tasks.Push("Recolectado: " + collision.gameObject.name);
            coleccionablesRecolectados++;  // Incrementa el contador de coleccionables
            Debug.Log("Recolectado: " + collision.gameObject.name);

            // Destruir o desactivar el coleccionable recolectado
            Destroy(collision.gameObject);

            // Si se recolectan todos los coleccionables, desbloquear la meta
            if (coleccionablesRecolectados >= totalColeccionables && !isMetaUnlocked)
            {
                if (metaObject != null)
                {
                    metaObject.SetActive(true);
                    Debug.Log("¡Meta desbloqueada! Todos los coleccionables recolectados.");
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
            Debug.Log("Bloques destructibles eliminados al tocar la meta!");
        }
    }
}