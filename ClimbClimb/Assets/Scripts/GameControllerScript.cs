using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControllerScript : MonoBehaviour {

    public GameObject ob;
    public GameObject sob;
    public GameObject spawnRight;
    public GameObject spawnLeft;
    public GameObject obstacle;

    public bool rightSide;
    public bool gameOver;

    bool spawned;
    float seconds;
    int side;

	// Use this for initialization
	void Start () {
        spawned = true;
        rightSide = true;
        gameOver = false;

        //GameObject.Find("PointCounter").GetComponent<TextMesh>().text="Dupa";

        Instantiate(ob, new Vector2(sob.transform.position.x, sob.transform.position.y), Quaternion.identity); 

	}
	
	// Update is called once per frame
	void Update () {
        if (gameOver == false)
        {
            if (spawned == true)
            {
                StartCoroutine("Spawn");
            }
        }
		
	}

    IEnumerator Spawn()
    {
        spawned = false;
        seconds = Random.Range(.5f, 1);
        yield return new WaitForSeconds(seconds);
        if (rightSide == true)
        {
            Instantiate(obstacle, new Vector2(spawnRight.transform.position.x, spawnRight.transform.position.y),Quaternion.identity);
            obstacle.GetComponent<Rigidbody2D>().gravityScale = Random.Range(5, 10);
        }
        else
        {
            Instantiate(obstacle, new Vector2(spawnLeft.transform.position.x, spawnLeft.transform.position.y), Quaternion.identity);
            obstacle.GetComponent<Rigidbody2D>().gravityScale = Random.Range(5, 10);
        }
        spawned = true;
    }

    public void SetGameOver(bool over)
    {
        gameOver = over;
    }

    public bool GetGameOver()
    {
        return gameOver;
    }
}
