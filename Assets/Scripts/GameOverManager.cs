using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI[] letters;
    [SerializeField] private TextMeshProUGUI leaderboardText;
    [SerializeField] private CanvasGroup leaderboard;
    [SerializeField] private InputActionReference activate;
    [SerializeField] private InputActionReference navigate;
    
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
        
        int old = letters[selected].text[0];
        int newNum = (old + (int)toDo.y - 39 ) % 26 + 65;
        letters[selected].text = ((char)newNum).ToString();
        
        letters[selected].color = Color.white;
        selected = Mathf.Clamp(selected + (int)toDo.x,0,2);
        letters[selected].color = Color.red;
    }

    private void Activate(InputAction.CallbackContext ctx)
    {
        LeaderBoardManager lbm = GameManager.Instance.GetComponent<LeaderBoardManager>();
        lbm.AddToBoard(GetName(), finalPoints);
        lbm.Save();
        List<LeaderBoard.LeaderboardDataEntry> top5 =  lbm.GetTop5();
        string tmp = "";
        foreach (LeaderBoard.LeaderboardDataEntry entry in top5)
        {
            tmp += $"{entry.name}: {entry.score}\n";
        }
        leaderboardText.text = tmp;
        cg.alpha = 0;
        leaderboard.alpha = 1;
        Invoke(nameof(Application.Quit), 5f);
    }

    private string GetName()
    {
        return letters[0].text + letters[1].text + letters[2].text;
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
