using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullets : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isPlayerBullet;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isPlayerBullet)
        {
            rb.velocity = new Vector2(0, rb.velocity.y + 0.01f);
            return;
        }
        rb.velocity = new Vector2(0, rb.velocity.y - 0.01f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Divided because of the Point System for later
        
        if (other.gameObject.CompareTag("EnemyZone"))
        {
            Destroy(gameObject);
            return;
        }

        if (CompareTag("PlayerZone"))
        {
            Destroy(gameObject);
            return;
        }
    }
}
