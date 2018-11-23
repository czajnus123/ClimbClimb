using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

    private GameControllerScript gameController;
    private float speed=1f;

	// Use this for initialization
	void Start () {
        if (gameObject.tag == "Obstacle") speed = 7f;
        else if (gameObject.tag == "Tunnel") speed = 4f;
        else if (gameObject.tag == "Coin") speed = 2f;

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
        else
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
