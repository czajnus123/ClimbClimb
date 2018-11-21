using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManagerScript : MonoBehaviour {

    //W tym miejscu trzeba stworzyc publiczna zmienną z textem: public TextMeshPro scoreText
    //W tym miejscu trzeba stworzyc publiczna zmienną z textem: public TextMeshPro hiScoreText

    public float scoreCount;
    public float hiScoreCount;
    public float pointsPerSecond;

    public bool scoreIncreasing;

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

            if (scoreCount > hiScoreCount)
            {
                hiScoreCount = scoreCount;
            }

            //W tym miejscu zrobic staly update textu od punktow: scoreText.text=scoreCount.Mathf.Round(scoreCount)
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
