using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Header("Configuración del Pool")]
    public GameObject pooledObject;
    public int poolSize;

    private List<GameObject> objectPool;

    private void Start()
    {
        objectPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }
}