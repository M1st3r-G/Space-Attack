using System.Linq;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private InputActionReference activate;
    [SerializeField] private InputActionReference navigate;
    [SerializeField] private TextMeshProUGUI[] letters;
    [SerializeField] private LeaderBoard board;
    private int selected;

    private int finalPoints;
    
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
        Vector2 toDo = navigate.action.ReadValue<Vector2>();

        
        //Maybe Wrap
        int old = (int)(letters[selected].text[0]);
        int newNum = (int)Mathf.Clamp(old + toDo.y, 65, 90);
        letters[selected].text = ((char)newNum).ToString();
        
        letters[selected].color = Color.white;
        selected = Mathf.Clamp(selected + (int)toDo.x,0,2);
        letters[selected].color = Color.red;
    }

    private void Activate(InputAction.CallbackContext ctx)
    {
         AddToLeaderBoard($"{letters[0].text}{letters[1].text}{letters[2].text}", finalPoints);
    }

    private void AddToLeaderBoard(string name, int points)
    {
        board.AddToLeaderboard(name, points);
        infoText.text = board.getTop3().ToString();
        Invoke(nameof(Application.Quit), 5f);
    }
    
    private void OnGameOver(int points)
    {
        cg.alpha = 1;
        Time.timeScale = 0;
        infoText.text = $"You had {points} Points";
        letters[0].color = Color.red;
        finalPoints = points;
        
        activate.action.Enable();
        navigate.action.Enable();
        activate.action.performed += Activate;
        navigate.action.performed += Navigate;
    }
}
