using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText;

    public GameObject ADCountCircle;

    private int highScore;
    private int score;

    PointManagerScript pointManager;
    GameControllerScript gameController;
    // Use this for initialization
    void Start () {

        pointManager = GameObject.Find("PointManager").GetComponent<PointManagerScript>();
        gameController = GameObject.Find("mainObject").GetComponent<GameControllerScript>();
		
	}
	
	// Update is called once per frame
	void Update () {
        highScore = (int)PlayerPrefs.GetFloat("highscore", 0);
        highScoreText.text = "Best: " +highScore.ToString();

        score = (int)pointManager.GetPoints();
        scoreText.text = score.ToString();

        if (gameController.GetGameOver() == true && gameController.GetEndMenuBool() == true)
        {

            ADCountCircle.GetComponent<Image>().fillAmount = ADCountCircle.GetComponent<Image>().fillAmount - .2f * Time.deltaTime;
            if (ADCountCircle.GetComponent<Image>().fillAmount <= 0)
                ADCountCircle.SetActive(false);
        }
        
	}

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
