using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    //ComponentReferences
    private Rigidbody2D rb;
    [SerializeField] private EnemyStats stats;
    //Params
    //Temps
    private float startingX;
    private float lineTimer;
    private float shootingTimer;
    //Publics
    public static event Action<Enemy> OnHit;
    public GameObject enemyBullet;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right;
        startingX = transform.position.x;
    }

    public void SetStats(EnemyStats newStats)
    {
        stats = newStats;
        GetComponent<SpriteRenderer>().sprite = newStats.GetImage();
        rb.velocity = rb.velocity.normalized * newStats.GetDefaultSpeed();
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x - startingX) > 2) rb.velocity *= -1;

        shootingTimer += Time.deltaTime;
        if (shootingTimer > stats.GetDefaultShootingSpeed())
        {
            shootingTimer = 0f;
            Shoot();
        }

        lineTimer += Time.deltaTime;
        if (lineTimer > stats.GetDefaultLineTimer())
        {
            transform.position -= 0.5f * Vector3.up;
            lineTimer = 0f;
        }
    }

    private void Shoot()
    {
        if (Random.Range(0f, 1f) > stats.GetShootingProbability()) return;
        var bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity).GetComponent<Bullets>();
        bullet.SetSpeed(stats.GetBulletSpeed());
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.SetActive(false);
        OnHit?.Invoke(this);
    }
}
