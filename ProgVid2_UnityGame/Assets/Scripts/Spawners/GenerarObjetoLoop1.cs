using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorObjetoLoop1 : MonoBehaviour
{
    [Header("Configuración del Generador")]
    [SerializeField][Range(0.5f, 20f)] private float tiempoEspera;
    [SerializeField][Range(0.5f, 20f)] private float tiempoIntervalo;

    [Header("Configuración del Movimiento del Objeto")]
    [SerializeField] private Vector2 direccionMovimiento = Vector2.up;
    [SerializeField] private float velocidadMovimiento = 5f;

    private ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = GetComponent<ObjectPooler>();
        InvokeRepeating(nameof(GenerarObjetoLoop1), tiempoEspera, tiempoIntervalo);
    }

    void GenerarObjetoLoop1()
    {
        GameObject obj = objectPooler.GetPooledObject();
        if (obj != null)
        {
            obj.transform.position = transform.position;
            obj.transform.rotation = Quaternion.identity;
            obj.SetActive(true);

            StartCoroutine(MoverObjeto(obj));
        }
    }

    private IEnumerator MoverObjeto(GameObject obj)
    {
        while (obj.activeInHierarchy)
        {
            obj.transform.Translate(direccionMovimiento.normalized * velocidadMovimiento * Time.deltaTime);
            yield return null;
        }
    }
}