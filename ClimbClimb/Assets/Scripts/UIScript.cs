using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI restartCountText;


    public GameObject ADCountCircle;

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

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartRestartCount()
    {
        startRestartCount = true;
    }
}
