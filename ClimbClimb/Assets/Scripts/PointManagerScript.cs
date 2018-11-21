using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManagerScript : MonoBehaviour {

    //W tym miejscu trzeba stworzyc publiczna zmienną z textem: public TextMeshPro scoreText

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

            if (gameController.GetGameOver() == true && gameController.GetEndMenuBool() == true)
            {
                //Zapis highscore do pliku
            }
        }
		
	}
}
