using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    private Vector2 direction;

    void Start()
    {
        direction = Vector2.right;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}