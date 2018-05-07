using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
public class LoadScene : MonoBehaviour {

	[SerializeField]
	private Animator animator;

    private AsyncOperation m_asyncLoading;

    private void Start()
    {
        m_asyncLoading = SceneManager.LoadSceneAsync("Game");
        m_asyncLoading.allowSceneActivation = false;
    }
	public void ChangeScene() {
		animator.SetTrigger("PlayTitle");
        m_asyncLoading.allowSceneActivation = true;
	}
}
