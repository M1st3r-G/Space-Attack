using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardManager : MonoBehaviour
{
    private const string LeaderboardDataKey = "LeaderboardData";
    [SerializeField] LeaderBoard leaderBoard;
    [SerializeField] private int showNTopPlayers;
    
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

    public List<LeaderBoard.LeaderboardDataEntry> GetTopN(int n)
    {
        return leaderBoard.GetTopN(n);
    }

    public void Save()
    {
        string json = leaderBoard.ToJson();
        PlayerPrefs.SetString(LeaderboardDataKey, json);
    }
}
