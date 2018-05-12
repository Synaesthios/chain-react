using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
public class LoadScene : MonoBehaviour {

    public static int gameSongIndex;

	[SerializeField]
	private Animator animator;

    private AsyncOperation m_asyncLoading;

    private void Start()
    {
        m_asyncLoading = SceneManager.LoadSceneAsync("Game");
        m_asyncLoading.allowSceneActivation = false;
    }
    public void ChangeSceneWithSongIndex(int songIndex)
    {
        gameSongIndex = songIndex;
        animator.SetTrigger("PlayTitle");
        InvokeRepeating("CheckAnimationDone", 0, .5f);
    }

    private void CheckAnimationDone()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            m_asyncLoading.allowSceneActivation = true;
        }
    }
}
