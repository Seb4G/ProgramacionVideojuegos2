using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorObjeto : MonoBehaviour
{
    [SerializeField] private GameObject objetoPrefab;

    [SerializeField]
    [Range(0.5f, 5f)]
    private float tiempoEspera;

    void Start1()
    {
        Invoke("GenerarObjeto", tiempoEspera);
    }

    void GenerarObjeto()
    {
        Instantiate(objetoPrefab, transform.position, Quaternion.identity);
    }
}