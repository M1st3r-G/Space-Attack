using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //ComponentReferences
    private Rigidbody2D rb;
    [SerializeField] InputActionReference movement;
    [SerializeField] InputActionReference shoot;
    //Params
    [SerializeField] float speed = 10f; //10f fürs Debugging
    //Temps
    private float input;
    private Vector3 abovePlayer;
    //Publics
    public GameObject playerBullet;
    
    //TODO: Player Bullet
     
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        abovePlayer = transform.position;
        abovePlayer.y += 1;
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

    private void Update()
    {
        input = movement.action.ReadValue<float>();
        rb.velocity = new Vector2(input * speed, 0);
    }

    private void Shoot(InputAction.CallbackContext ctx)
    {
        Instantiate(playerBullet, abovePlayer, Quaternion.identity);
    }
}