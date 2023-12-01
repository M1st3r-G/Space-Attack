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

    private void FixedUpdate()
    {
        int dir = isPlayerBullet ? 1 : -1;
        rb.velocity = new Vector2(0, rb.velocity.y + dir * 0.01f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
