using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

    private float speed=1f;
    bool spawned;

	// Use this for initialization
	void Start () {
        spawned = false;
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

        GameControllerScript.Instance.SetSpawnCoinCounter();

        if (GameControllerScript.Instance.spawnCoinCounter >= 10)
        {
            try
            {
                gameObject.transform.Find("Coin").gameObject.SetActive(true);
            }
            catch
            {

            }
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        if (GameControllerScript.Instance.gameOver == false)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (spawned == false)
            {
                if (gameObject.tag=="Obstacle" && gameObject.transform.position.y <= GameObject.Find("spawnTrigger").transform.position.y)
                {
                    GameControllerScript.Instance.toSpawn=true;
                    spawned = true;
                }
                if (gameObject.tag == "Tunnel" && gameObject.transform.position.y <= GameObject.Find("spawnTriggerT").transform.position.y)
                {
                    GameControllerScript.Instance.toSpawn=true;
                    spawned = true;
                }
            }
        }
        else if (GameControllerScript.Instance.deathCount>1)
        {
           Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Coin" && collision.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 1);

            Destroy(gameObject);
        }
        else if (gameObject.tag == "Coin" && collision.gameObject.tag == "End")
            Destroy(gameObject);

    }
}
