using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameOverTracker : MonoBehaviour {

    private const string LEADERBOARD_HOST = "https://chain-reaction-leaderboard.herokuapp.com/leaderboards";
	public PlayerScript player;
	public Text gameOverText;
    public GameObject gameOverUI;
    public InputField highScoreInput;
    public GameObject highScoreUI;
    public ScoreTracker scoreTracker;

    private bool showLeaderboard;
	void Update () {
		if (player.isDead() && !showLeaderboard) {
			gameOverText.enabled = true;
            gameOverUI.SetActive(true);

        }
	}

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void SubmitHighScore()
    {
        if (string.IsNullOrEmpty(highScoreInput.text))
            return;

        StartCoroutine(SubmitScoreToAPI());
    }

     private IEnumerator SubmitScoreToAPI() {
        LeaderBoardEntryResponse leaderBoardEntry = new LeaderBoardEntryResponse();
        leaderBoardEntry.levelId = LoadScene.gameSongIndex.ToString();
        leaderBoardEntry.player = highScoreInput.text;
        leaderBoardEntry.score = scoreTracker.score.ToString();
        Debug.Log(leaderBoardEntry.levelId);
        var request = new UnityWebRequest(LEADERBOARD_HOST, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(LeaderBoardEntryResponse.toJson(leaderBoardEntry));
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if(request.isNetworkError || request.isHttpError) {
            Debug.Log(request.error);
        }
        showLeaderboard = true;
        gameOverText.enabled = false;
        gameOverUI.SetActive(false);
        highScoreUI.SetActive(true);
    }
}
