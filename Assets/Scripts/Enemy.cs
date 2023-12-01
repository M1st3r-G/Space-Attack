using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    //ComponentReferences
    private Rigidbody2D rb;
    [SerializeField] private EnemyStats stats;

    //Params
    //Temps
    private float startingX;
    private float lineTimer = 0f;
    //Publics
    public static event Action<Enemy> OnHit; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startingX = transform.position.x;
    }

    public void setStats(EnemyStats newStats)
    {
        stats = newStats;
        GetComponent<SpriteRenderer>().sprite = newStats.getImage();
        rb.velocity = rb.velocity.normalized * newStats.getDefaultSpeed();
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x - startingX) > 2)
        {
            rb.velocity *= -1;
        }

        lineTimer += Time.deltaTime;
        if (lineTimer > stats.getDefaultShootingSpeed())
        {
            //TODO ADD BULLET;
            lineTimer = 0f;
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        gameObject.SetActive(false);
        OnHit?.Invoke(this);
    }
}
