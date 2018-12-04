﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour {

    UIScript uiScript;

	// Use this for initialization
	void Start () {

        uiScript= GameObject.Find("mainObject").GetComponent<UIScript>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RegularAd()
    {
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video", new ShowOptions() { resultCallback=RegularAdResult});
        }
    }

    public void RewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo",new ShowOptions() { resultCallback=RewardedAdResult});
        }
    }

    private void RewardedAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                GameObject.Find("Panel").SetActive(false);
                uiScript.StartRestartCount();
                break;
            case ShowResult.Skipped:
                Debug.Log("skipped");
                break;
            case ShowResult.Failed:
                Debug.Log("failed");
                break;

        }
    }

    private void RegularAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                SceneManager.LoadScene("SampleScene");
                break;
            case ShowResult.Skipped:
                SceneManager.LoadScene("SampleScene");
                break;
            case ShowResult.Failed:
                Debug.Log("failed");
                break;

        }
    }
}