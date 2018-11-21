using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour {

    private float seconds;

    private bool spawned;

    public GameObject spawnRight;
    public GameObject spawnLeft;
    public GameObject basicObstacle;

    private GameControllerScript gameController;
    


	// Use this for initialization
	void Start () {

        gameController = GameObject.Find("mainObject").GetComponent<GameControllerScript>();
        spawned = true;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (gameController.GetGameOver() == false)
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
        if (gameController.GetRightSideBool() == true)
        {
            Instantiate(basicObstacle, new Vector2(spawnRight.transform.position.x, spawnRight.transform.position.y), Quaternion.identity);
            basicObstacle.GetComponent<Rigidbody2D>().gravityScale = Random.Range(5, 10);
        }
        else
        {
            Instantiate(basicObstacle, new Vector2(spawnLeft.transform.position.x, spawnLeft.transform.position.y), Quaternion.identity);
            basicObstacle.GetComponent<Rigidbody2D>().gravityScale = Random.Range(5, 10);
        }
        spawned = true;
    }
}
