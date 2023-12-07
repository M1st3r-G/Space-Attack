using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardManager : MonoBehaviour
{
    private const string LeaderboardDataKey = "LeaderboardData";
    [SerializeField] LeaderBoard leaderBoard;
    
    
    private void Awake()
    {
        // load leaderboard
        string json = PlayerPrefs.GetString(LeaderboardDataKey, null);

        leaderBoard = string.IsNullOrEmpty(json) ? new LeaderBoard() : LeaderBoard.FromJson(json);
    }
    
    public void AddToBoard(string pName, int points)
    {
        leaderBoard.AddToLeaderBoard(pName, points);
    }

    public List<LeaderBoard.LeaderboardDataEntry> GetTop5()
    {
        return leaderBoard.GetTop5();
    }

    public void Save()
    {
        string json = leaderBoard.ToJson();
        PlayerPrefs.SetString(LeaderboardDataKey, json);
    }
}
