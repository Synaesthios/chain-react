using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverTracker : MonoBehaviour {

	public PlayerScript player;
	public Text gameOverText;
    public GameObject gameOverUI;
    public InputField highScoreInput;
    public GameObject highScoreUI;

	void Update () {
		if (player.isDead()) {
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

        gameOverText.enabled = false;
        highScoreUI.SetActive(true);
    }
}
