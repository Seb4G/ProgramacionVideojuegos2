using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject[] bloquesDestructibles = GameObject.FindGameObjectsWithTag("BloquesDestructibles");
            foreach (GameObject bloque in bloquesDestructibles)
            {
                Destroy(bloque);
            }
            Debug.Log("Bloques destructibles eliminados!");
        }
    }
}
    