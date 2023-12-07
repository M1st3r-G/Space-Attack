using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class LeaderBoard
{
    public List<LeaderboardDataEntry> leaderboardDataEntries = new();
    
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public static LeaderBoard FromJson(string json)
    {
        return JsonUtility.FromJson<LeaderBoard>(json);
    }

    public void AddToLeaderBoard(string name, int points)
    {
        leaderboardDataEntries.Add(new LeaderboardDataEntry(name, points));
        leaderboardDataEntries = leaderboardDataEntries.OrderBy(o => o.score).ToList();
        leaderboardDataEntries.Reverse();
        if (leaderboardDataEntries.Count > 10) leaderboardDataEntries.RemoveAt(10);
    }
    
    public List<LeaderboardDataEntry> GetTopN(int n)
    {
        return leaderboardDataEntries.GetRange(0, leaderboardDataEntries.Count < n ? leaderboardDataEntries.Count : n);
    }
    
    [Serializable]
    public class LeaderboardDataEntry
    {
        public string name;
        public int score;
        
        public LeaderboardDataEntry(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }

}