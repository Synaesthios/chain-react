﻿using System.Collections;
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
	public void ChangeScene() {
		animator.SetTrigger("PlayTitle");
        m_asyncLoading.allowSceneActivation = true;
	}
    public void ChangeSceneWithSongIndex(int songIndex)
    {
        gameSongIndex = songIndex;
        animator.SetTrigger("PlayTitle");
        m_asyncLoading.allowSceneActivation = true;
    }
}
