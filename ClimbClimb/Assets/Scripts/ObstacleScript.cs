using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

    private GameControllerScript gameController;
    private float speed=1f;

	// Use this for initialization
	void Start () {
        if (gameObject.tag == "Obstacle")
        {
            speed = 7f;
            if (GameObject.Find("PointManager").GetComponent<PointManagerScript>().GetPoints() * .1f < 13)
                gameObject.transform.Find("Obstacle").gameObject.transform.localScale += new Vector3((float)(.1f * GameObject.Find("PointManager").GetComponent<PointManagerScript>().GetPoints()), 0, 0);
            else
                gameObject.transform.Find("Obstacle").gameObject.transform.localScale += new Vector3(13, 0, 0);
        }
        else if (gameObject.tag == "Tunnel") speed = 4f;
        else if (gameObject.tag == "Coin") speed = 2f;
        
        speed+= (float) (.05f* GameObject.Find("PointManager").GetComponent<PointManagerScript>().GetPoints());
        if (speed > 20) speed = 20;

        gameController = GameObject.Find("mainObject").GetComponent<GameControllerScript>();

        gameController.SetSpawnCoinCounter();

        if (gameController.GetSpawnCoinCounter() >= 10)
        {
            gameObject.transform.Find("Coin").gameObject.SetActive(true);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        if (gameController.GetGameOver() == false)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        else if (gameController.GetDeathCount()>1)
        {
           Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Coin" && collision.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) + 1);

            Destroy(gameObject);
        }

    }
}
