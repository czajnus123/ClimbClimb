using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIScript : MonoBehaviour {
    public TextMeshProUGUI highScoreText, scoreText, restartCountText, shopCoins;


    public GameObject ADCountCircle, shopBackground, shopUI, noAdsButton, texts, shopButton, shopBg;
    public InfiniteScrollScript infScript;

    private int highScore, score;
    private float time;

    public bool startRestartCount;

    PointManagerScript pointManager;
    GameControllerScript gameController;
    AdManager adManager;
    // Use this for initialization
    void Awake () {
        time = 3;
        startRestartCount = false;
        pointManager = GameObject.Find("PointManager").GetComponent<PointManagerScript>();
        gameController = GameControllerScript.Instance;
        adManager = GameObject.Find("mainObject").GetComponent<AdManager>();
        infScript = shopBg.GetComponent<InfiniteScrollScript>();
        shopCoins.text = gameController.coinCount.ToString();
		
	}
	
	// Update is called once per frame
	void Update () {
        shopCoins.text = gameController.coinCount.ToString();
        highScore = (int)PlayerPrefs.GetFloat("highscore", 0);
        highScoreText.text = "Best: " +highScore.ToString();

        score = (int)pointManager.GetPoints();
        scoreText.text = score.ToString();

        if (gameController.gameOver == true)
        {
            if (gameController.endMenu == true)
            {

                ADCountCircle.GetComponent<Image>().fillAmount = ADCountCircle.GetComponent<Image>().fillAmount - .2f * Time.deltaTime;
                if (ADCountCircle.GetComponent<Image>().fillAmount <= 0)
                    ADCountCircle.SetActive(false);
            }

            if (startRestartCount == true)
            {
                time = time - 1 * Time.deltaTime;
                restartCountText.text = Mathf.Round(time).ToString();
                if (time <= 0)
                {
                    restartCountText.text = "";
                    GameObject.Find("Canvas").transform.Find("Texts").gameObject.SetActive(true);
                    gameController.gameOver=false;
                    startRestartCount = false;
                }
            }
        }
        
	}

    public void RestartGame()
    {
        var ad =Random.RandomRange(1, 5);
        var adOn = false;
        if (ad >= 4)
        {
            adManager.RegularAd();
            adOn = true;
        }
        if (adOn == false)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void ShowShop()
    {
        infScript.Init();
        texts.SetActive(false);
        shopUI.SetActive(true);
        noAdsButton.SetActive(false);
        shopButton.SetActive(false);
        shopBackground.SetActive(true);
        gameController.shopActive=true;
        GameObject.Find("SPlayer").GetComponent<SpriteRenderer>().sprite = GameObject.Find("Player(Clone)").GetComponent<SpriteRenderer>().sprite;

    }
    public void HideShop()
    {
        GameObject.Find("Player(Clone)").GetComponent<SpriteRenderer>().sprite = GameObject.Find("SPlayer").GetComponent<SpriteRenderer>().sprite;
        PlayerPrefs.SetString("skin", GameObject.Find("Player(Clone)").GetComponent<SpriteRenderer>().sprite.ToString());
        PlayerPrefs.SetInt("CurrentSkin", gameController.currentSkinIndex);
       // PlayerPrefs.SetInt("CurrentSkin", )
        texts.SetActive(true);
        shopUI.SetActive(false);
        noAdsButton.SetActive(true);
        shopButton.SetActive(true);
        shopBackground.SetActive(false);
        gameController.shopActive=false;
        GameControllerScript.Instance.check = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartRestartCount()
    {
        startRestartCount = true;
    }

    public void SetPlayerSkin()
    {
        var si = GameControllerScript.Instance.currentSkinIndex;
        Sprite s= EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;
        for (int i=0; i < GameControllerScript.Instance.skins.Length; i++)
        {
            if (s.name == GameControllerScript.Instance.skins[i].name)
                GameControllerScript.Instance.currentSkinIndex = i;
        }

        if ((GameControllerScript.Instance.skinAvailability & 1 << GameControllerScript.Instance.currentSkinIndex) == 1 << GameControllerScript.Instance.currentSkinIndex)
        {
            GameObject.Find("SPlayer").GetComponent<SpriteRenderer>().sprite = s;
            Debug.Log(1 << GameControllerScript.Instance.currentSkinIndex);
        }
        else
        {
            GameControllerScript.Instance.currentSkinIndex = si;
        }
    }

    public void RollSkin()
    {
        if (GameControllerScript.Instance.coinCount >= 10)
        {
            GameControllerScript.Instance.coinCount -= 10;
            int r = Random.RandomRange(1, GameControllerScript.Instance.skins.Length);
            if ((GameControllerScript.Instance.skinAvailability & 1 << r) == 1 << r)
            {
            }
            else
            {
                GameControllerScript.Instance.skinAvailability += 1 << r;
            }
            var sname= GameControllerScript.Instance.skins[r].name;
            Debug.Log(sname);
            var ob = GameObject.Find("Content");
            for(int i = 0; i < GameControllerScript.Instance.skins.Length; i++)
            {
                if (ob.transform.GetChild(i).GetComponent<Image>().sprite.name == sname)
                {
                    ob.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                } 
            }
            GameControllerScript.Instance.Save();
        }
    }
    public void ResetPlayerPrefs()
    {
        gameController.ResetSaves();
        Application.Quit();
    }
}
