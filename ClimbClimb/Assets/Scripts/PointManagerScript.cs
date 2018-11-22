using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointManagerScript : MonoBehaviour {

    //W tym miejscu trzeba stworzyc publiczna zmienną z textem: public TextMeshPro scoreText
    //W tym miejscu trzeba stworzyc publiczna zmienną z textem: public TextMeshPro hiScoreText
    public TextMeshProUGUI textScore;

    public float scoreCount; // aktualny wynik
    public float hiScoreCount;
    public float pointsPerSecond;

    public bool scoreIncreasing;

    int temp;

    GameControllerScript gameController;

	// Use this for initialization
	void Start () {

        gameController = GameObject.Find("mainObject").GetComponent<GameControllerScript>();

	}
	
	// Update is called once per frame
	void Update () {

        if (gameController.GetGameOver() == false)
        {
            
            scoreCount += pointsPerSecond * Time.deltaTime;
            temp += 1;

            if (scoreCount > hiScoreCount)
            {
                hiScoreCount = scoreCount;
            }

            textScore.text = Mathf.Round(scoreCount).ToString();

            //W tym miejscu zrobic staly update textu od hiScore: hiScoreText.text=hiscoreCount.Mathf.Round(hiScoreCount)

            if (gameController.GetGameOver() == true && gameController.GetEndMenuBool() == true)
            {
                //Zapis highscore do pliku
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
