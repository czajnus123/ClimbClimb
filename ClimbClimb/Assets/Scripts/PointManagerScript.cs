using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointManagerScript : MonoBehaviour {

    public TextMeshProUGUI scoreText, coinText, positionText;

    public float scoreCount, hiScoreCount, pointsPerSecond, scoreCountPrevious;

    private int coinCount;

    public bool scoreIncreasing;
    bool scoreAnim;

    int temp;

	// Use this for initialization
	void Start () {
       
        coinCount = 0;
        scoreCountPrevious = 0;
        scoreAnim = false;

	}
	
	// Update is called once per frame
	void Update () {

        if (GameControllerScript.Instance.gameOver == false)
        {
           // scoreCount += pointsPerSecond * Time.deltaTime;
            if (scoreAnim == false)
                StartCoroutine(ShowText());

            temp += 1;

            if (scoreCount > hiScoreCount)
            {
                hiScoreCount = scoreCount;
            }
            

            coinText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
            positionText.text ="#" + PlayerPrefs.GetInt("position", 1000).ToString();

            if (GameControllerScript.Instance.gameOver== true && GameControllerScript.Instance.endMenu == true)
            {
                PlayerPrefs.SetFloat("highscore", hiScoreCount);
            }
        }
		
	}

    IEnumerator ShowText()
    {
        scoreAnim = true;
        scoreCount += pointsPerSecond;
        scoreText.GetComponent<Animator>().SetTrigger("ShowText");

        scoreText.text = Mathf.Round(scoreCount).ToString();

        yield return new WaitForSeconds(1.0f);
        

        scoreAnim = false;
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
