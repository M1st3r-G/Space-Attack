using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/LeaderBoard")]
public class LeaderBoard : ScriptableObject
{
    private List<string> names = new();
    private List<int> points = new();

    public void AddToLeaderboard(string p_name, int score)
    {
        for (int i = 0; i < 10; i++)
        {
            if (score <= points[i]) continue;
            names.Insert(i,p_name);
            points.Insert(i,score);
        }
    }
    
    public List<Tuple<string,int>> getTop3()
    {
        List<Tuple<string, int>> ret = new();
        for (int i = 0; i < 3; i++)
        {
            ret.Append(new Tuple<string, int>(names[i], points[i]));
        }

        return ret;
    }
}
