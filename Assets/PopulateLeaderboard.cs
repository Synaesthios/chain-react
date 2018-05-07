using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.UI;


public class PopulateLeaderboard : MonoBehaviour {

	private const string LEADERBOARD_HOST = "https://chain-reaction-leaderboard.herokuapp.com/leaderboards"; 
    private const string GET_ALL_SCORES = "GET_ALL_SCORES";
    [SerializeField]
    private GameObject leaderboardPrefab;
    void Start() {
        GetScores(GET_ALL_SCORES);
    }

    public void GetScores(string levelId) {
        StartCoroutine(GetScoresFromAPI(levelId));
    }

    private IEnumerator GetScoresFromAPI(string levelId) {
        string levelFilter = "";
        if (levelId != GET_ALL_SCORES) {
            levelFilter = "&levelId=" + levelId; 
        }
        
        UnityWebRequest www = UnityWebRequest.Get(LEADERBOARD_HOST + "?$sort[score]=-1" + levelFilter);
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            var children = new List<GameObject>();
            foreach (Transform child in transform) children.Add(child.gameObject);
            children.ForEach(child => Destroy(child));

            // Or retrieve results as binary data
            LeaderboardsListResponse results = 
                LeaderboardsListResponse.CreateFromJSON(www.downloadHandler.text);
            
            results.data.ForEach(leaderBoardEntry => CreateLeaderboardEntry(leaderBoardEntry));
        }
    }

    private void CreateLeaderboardEntry(LeaderBoardEntryResponse leaderBoardEntry) {
        GameObject newEntry = Instantiate(leaderboardPrefab);
        newEntry.transform.SetParent(gameObject.transform, false);
        newEntry.GetComponent<LeaderboardEntryUI>().name.text = leaderBoardEntry.player;
        newEntry.GetComponent<LeaderboardEntryUI>().score.text = leaderBoardEntry.score;
    }

        
}
