using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullets : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private bool isPlayerBullet;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        int dir = isPlayerBullet ? 1 : -1;
        rb.velocity = new Vector2(0, rb.velocity.y + dir * speed);
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
