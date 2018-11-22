using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour {

    private float obstacleType;   //if 0 basic, else if 1 tunnel

    private float seconds;
    private float points;

    private bool toSpawn;
    private bool changeSpawnCounter;
    private bool spawnBasic;
    private bool spawnTunnel;
    float side; //if 0 - left, else if 1 - right 

    public GameObject spawnRight;
    public GameObject spawnLeft;
    public GameObject spawnMid;
    public GameObject basicObstacle;
    public GameObject tunnelRightObstacle;
    public GameObject tunnelLeftObstacle;

    private GameControllerScript gameController;
    private PointManagerScript pointManager;
    


	// Use this for initialization
	void Start () {

        points = 0;

        gameController = GameObject.Find("mainObject").GetComponent<GameControllerScript>();
        pointManager = GameObject.Find("PointManager").GetComponent<PointManagerScript>();

        toSpawn = true;
        spawnBasic = true;
        spawnTunnel = false;
        changeSpawnCounter = true;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (gameController.GetGameOver() == false)
        {
            if (toSpawn == true)
            {
                if (obstacleType == 0)
                {
                    if (!FindByTag("Tunnel"))
                    {
                        StartCoroutine("SpawnBasic");
                    }
                    
                }
                else if (obstacleType == 1)
                {
                    StartCoroutine("SpawnTunnel");
                }
            }
            if (pointManager.GetPoints() >= points + 10)
            {
                points = pointManager.GetPoints();
                var ob = Random.Range(0f, 1f);
                obstacleType = Mathf.Round(ob);
                Debug.Log(ob+" change obstacle: " + obstacleType);
            }
        }
    }

    IEnumerator SpawnBasic()
    {
            toSpawn = false;
            seconds = Random.Range(.3f, .6f);
            side = Mathf.Round(Random.RandomRange(0f,1f));
            yield return new WaitForSeconds(seconds);
            if (side ==0)
            {
                Instantiate(basicObstacle, new Vector2(spawnLeft.transform.position.x, spawnLeft.transform.position.y), Quaternion.identity);
            }
            else
            {
                Instantiate(basicObstacle, new Vector2(spawnRight.transform.position.x, spawnRight.transform.position.y), Quaternion.identity);
            }
            toSpawn = true;
    }

    IEnumerator SpawnTunnel()
    {

        toSpawn = false;
        seconds = Random.Range(1, 3);
        side = Mathf.Round(Random.RandomRange(0f, 1f));
        Debug.Log("side "+side);
        yield return new WaitForSeconds(seconds);
        if (side==0)
        {
            Instantiate(tunnelLeftObstacle, new Vector2(spawnMid.transform.position.x, spawnMid.transform.position.y), Quaternion.identity);
        }
        else
        {
            Instantiate(tunnelRightObstacle, new Vector2(spawnMid.transform.position.x, spawnMid.transform.position.y), Quaternion.identity);
        }
        toSpawn = true;
    }

    private bool FindByTag(string tag)
    {
        try
        {
            GameObject[] gm;
            gm=GameObject.FindGameObjectsWithTag(tag);
            if (gm.Length > 0)
            {
             //   Debug.Log("tru");
                return true;
            }
            else
            {
              //  Debug.Log("fals");
                return false;
            }  
        }
        catch
        {
            Debug.Log("NoTunnel");
            return false;
        }
    }
}
