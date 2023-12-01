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
    [SerializeField] private float yOffset = 0.8f;
    [SerializeField] private float ShootCooldown;
    [SerializeField] private float invisTimer;
    //Temps
    private float input;
    private bool canShoot;
    private bool invinvible;
    //Publics
    public GameObject playerBullet;
    public static event Action OnPlayerHit; 
     
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        canShoot = true;
        invinvible = false;
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
        input = movement.action.ReadValue<float>();
        rb.velocity = new Vector2(input * speed, 0);
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
        if (invinvible) return;
        OnPlayerHit?.Invoke();
        invinvible = true;
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

        invinvible = false;
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