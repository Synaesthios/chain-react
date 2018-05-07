using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LeaderBoardEntryResponse
{
    public string player;
    public string score;
    public string levelId;

    public static LeaderBoardEntryResponse CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<LeaderBoardEntryResponse>(jsonString);
    }

    public static string toJson(LeaderBoardEntryResponse leaderBoardEntity) {
        return JsonUtility.ToJson(leaderBoardEntity);
    }
}