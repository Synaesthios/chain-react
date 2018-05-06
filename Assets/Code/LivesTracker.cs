using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesTracker : MonoBehaviour {

	public PlayerScript player;

	public Text livesText;
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		livesText.text = player.Health.ToString();
	}
}
