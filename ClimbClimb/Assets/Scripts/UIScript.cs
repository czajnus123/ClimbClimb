using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIScript : MonoBehaviour {
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartCountText;


    public GameObject ADCountCircle;
    public GameObject shopBackground;
    public GameObject shopUI;
    public GameObject noAdsButton;
    public GameObject texts;
    public GameObject shopButton;
    public GameObject shopBg;
    public InfiniteScrollScript infScript;

    private int highScore;
    private int score;
    private float time;

    public bool startRestartCount;

    PointManagerScript pointManager;
    GameControllerScript gameController;
    AdManager adManager;
    // Use this for initialization
    void Start () {
        time = 3;
        startRestartCount = false;
        pointManager = GameObject.Find("PointManager").GetComponent<PointManagerScript>();
        gameController = GameObject.Find("mainObject").GetComponent<GameControllerScript>();
        adManager = GameObject.Find("mainObject").GetComponent<AdManager>();
        infScript = shopBg.GetComponent<InfiniteScrollScript>();
		
	}
	
	// Update is called once per frame
	void Update () {
        highScore = (int)PlayerPrefs.GetFloat("highscore", 0);
        highScoreText.text = "Best: " +highScore.ToString();

        score = (int)pointManager.GetPoints();
        scoreText.text = score.ToString();

        if (gameController.GetGameOver() == true)
        {
            if (gameController.GetEndMenuBool() == true)
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
                    gameController.SetGameOver(false);
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
        gameController.SetShopState(true);
    }
    public void HideShop()
    {
        GameObject.Find("Player(Clone)").GetComponent<SpriteRenderer>().sprite = GameObject.Find("SPlayer").GetComponent<SpriteRenderer>().sprite;
        PlayerPrefs.SetString("skin", GameObject.Find("Player(Clone)").GetComponent<SpriteRenderer>().sprite.ToString());
        texts.SetActive(true);
        shopUI.SetActive(false);
        noAdsButton.SetActive(true);
        shopButton.SetActive(true);
        shopBackground.SetActive(false);
        gameController.SetShopState(false);
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
        GameObject.Find("SPlayer").GetComponent<SpriteRenderer>().sprite= EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;

    }
}
