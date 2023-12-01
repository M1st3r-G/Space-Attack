using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //ComponentReferences
    private Rigidbody2D rb;
    [SerializeField] private InputActionReference movement;
    [SerializeField] private InputActionReference shoot;
    //Params
    [SerializeField] private float speed = 10f; //10f fürs Debugging
    [SerializeField] private float yOffset = 0.5f;
    [SerializeField] private float ShootCooldown;
    [SerializeField] private float invisTimer;
    //Temps
    private bool canShoot;
    private bool invincible;
    //Publics
    public GameObject playerBullet;
    public GameObject playerExplosion;
    
    //TODO: Player dies and explodes in many pieces >:3 Muhahaha
    public static event Action OnPlayerHit; 
     
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        canShoot = true;
        invincible = false;
    }

    private void OnEnable()
    {
        movement.action.Enable();
        shoot.action.Enable();
        shoot.action.performed += Shoot;
    }

    private void OnDisable()
    {
        movement.action.Disable();
        shoot.action.performed -= Shoot;
        shoot.action.Disable();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.action.ReadValue<float>() * speed, 0);
    }

    private void Shoot(InputAction.CallbackContext ctx)
    {
        if (!canShoot) return;
        Instantiate(playerBullet, transform.position + new Vector3(0,yOffset,0), Quaternion.identity);
        canShoot = false;
        StartCoroutine(nameof(Reload));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (invincible) return;
        OnPlayerHit?.Invoke();
        invincible = true;
        Instantiate(playerExplosion, transform.position, Quaternion.identity);
        StartCoroutine(nameof(HealingTime));
    }

    private IEnumerator HealingTime()
    {
        float timer = 0f;
        while (timer < invisTimer)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        invincible = false;
    }
    private IEnumerator Reload()
    {
        float timer = 0f;
        while (timer < ShootCooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        canShoot = true;
    }
}