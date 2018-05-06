using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverTracker : MonoBehaviour {

	public PlayerScript player;
	public Text gameOverText;
	void Start () {
		
	}
	
	void Update () {
		if (player.isDead()) {
			gameOverText.enabled = true;
		}
	}
}
