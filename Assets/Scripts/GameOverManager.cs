using UnityEngine.InputSystem;
using TMPro;
using UnityEngine;


public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private InputActionReference activate;
    [SerializeField] private InputActionReference navigate;
    

    private CanvasGroup cg;
    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        GameManager.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= OnGameOver;
        activate.action.performed -= Activate;
        activate.action.Disable();
        navigate.action.Disable();
        
    }

    private void Navigate(InputAction.CallbackContext ctx)
    {
        print(navigate.action.ReadValue<Vector2>());
    }

    private void Activate(InputAction.CallbackContext ctx)
    {
        print("nice");
    }
    
    private void OnGameOver(int points)
    {
        cg.alpha = 1;
        Time.timeScale = 0;
        infoText.text = $"You had {points} Points";
        
        activate.action.Enable();
        navigate.action.Enable();
        activate.action.performed += Activate;
        navigate.action.performed += Navigate;
    }
}
