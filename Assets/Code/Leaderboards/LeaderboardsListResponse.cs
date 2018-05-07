using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LeaderboardsListResponse
{
    public List<LeaderBoardEntryResponse> data;
    public static LeaderboardsListResponse CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<LeaderboardsListResponse>(jsonString);
    }
}