using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
public class LoadScene : MonoBehaviour {

	[SerializeField]
	private Animator animator;

	public void ChangeScene() {
		animator.SetTrigger("PlayTitle");
		SceneManager.LoadSceneAsync("Game");
	}
}
