using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    //ComponentReferences
    private Rigidbody2D rb;
    //Params
    [SerializeField] private float speed;
    //Temps
    private float timer = 1f;
    //Publics

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * speed;
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer < 2) return;
        rb.velocity *= -1;
        timer = 0;
    }
}
