using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointManagerScript : MonoBehaviour {

    public TextMeshProUGUI scoreText, coinText, positionText;

    public float scoreCount, hiScoreCount, pointsPerSecond;

    private int coinCount;

    public bool scoreIncreasing;

    int temp;

	// Use this for initialization
	void Start () {
       
        coinCount = 0;

	}
	
	// Update is called once per frame
	void Update () {

        if (GameControllerScript.Instance.gameOver == false)
        {
            
            scoreCount += pointsPerSecond * Time.deltaTime;
            temp += 1;

            if (scoreCount > hiScoreCount)
            {
                hiScoreCount = scoreCount;
            }

            scoreText.text = Mathf.Round(scoreCount).ToString();
            coinText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
            positionText.text ="#" + PlayerPrefs.GetInt("position", 1000).ToString();

            if (GameControllerScript.Instance.gameOver== true && GameControllerScript.Instance.endMenu == true)
            {
                PlayerPrefs.SetFloat("highscore", hiScoreCount);
            }
        }
		
	}

    public float GetPoints()
    {
        return scoreCount;
    }

    public float GetHiScore()
    {
        return hiScoreCount;
    }
    public void SetHiScore(float hiScore)
    {
        hiScoreCount = hiScore;
    }
}
