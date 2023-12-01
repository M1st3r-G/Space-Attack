using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    //ComponentReferences
    private Rigidbody2D rb;

    [SerializeField] private EnemyStats stats;

    //Params
    //Temps
    private float timer = 1f;
    //Publics

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void setStats(EnemyStats newStats)
    {
        stats = newStats;
        GetComponent<SpriteRenderer>().sprite = newStats.getImage();
        rb.velocity = rb.velocity.normalized * newStats.getDefaultSpeed();
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer < 2) return;
        rb.velocity *= -1;
        timer = 0;
    }
}
