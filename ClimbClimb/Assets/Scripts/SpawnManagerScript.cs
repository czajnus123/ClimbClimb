using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour {

    private int obstacleType;   //if >1<5 basic if >5<10-tunnel

    private float seconds;
    private float points;

    private bool spawned;
    private bool changeSpawnCounter;
    private bool spawnBasic;
    private bool spawnTunnel;
    int side = 0;

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

        spawned = true;
        spawnBasic = true;
        spawnTunnel = false;
        changeSpawnCounter = true;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (gameController.GetGameOver() == false)
        {
            if (spawned == true)
            {
                if (obstacleType >=0&&obstacleType<=5)
                {
                    if (!FindByTag("Tunnel"))
                    {
                        StartCoroutine("SpawnBasic");
                    }
                    
                }
                else if (obstacleType > 5 && obstacleType <= 10)
                {
                    StartCoroutine("SpawnTunnel");
                }
            }
           /* if (changeSpawnCounter == true)
            {
                StartCoroutine("SpawnCounter");
            }*/
            if (pointManager.GetPoints() >= points + 5)
            {
               
                points = pointManager.GetPoints();
                obstacleType = Random.Range(0, 10);
                Debug.Log("change obstacle: "+obstacleType);
            }

        }

    }

    IEnumerator SpawnBasic()
    {
       // if (FindByTag("Tunnel"))
       // {
            spawned = false;
            seconds = Random.Range(.3f, .6f);
            side = Random.Range(0, 11);
            yield return new WaitForSeconds(seconds);
            if (side >= 0 && side <= 5)
            {
                Instantiate(basicObstacle, new Vector2(spawnRight.transform.position.x, spawnRight.transform.position.y), Quaternion.identity);
                //  basicObstacle.GetComponent<Rigidbody2D>().gravityScale = Random.Range(5, 10);
            }
            else
            {
                Instantiate(basicObstacle, new Vector2(spawnLeft.transform.position.x, spawnLeft.transform.position.y), Quaternion.identity);
                //  basicObstacle.GetComponent<Rigidbody2D>().gravityScale = Random.Range(5, 10);
            }
            spawned = true;
       // }
    }

    IEnumerator SpawnTunnel()
    {

        spawned = false;
        seconds = Random.Range(1, 3);
        side = Random.Range(0, 10);
        Debug.Log("side "+side);
        yield return new WaitForSeconds(seconds);
        if (side>=0&&side<=5)
        {
            Instantiate(tunnelLeftObstacle, new Vector2(spawnMid.transform.position.x, spawnMid.transform.position.y), Quaternion.identity);
           // basicObstacle.GetComponent<Rigidbody2D>().gravityScale = Random.Range(5, 10);
        }
        else if(side >=5&&side<=10)
        {
            Instantiate(tunnelRightObstacle, new Vector2(spawnMid.transform.position.x, spawnMid.transform.position.y), Quaternion.identity);
            //basicObstacle.GetComponent<Rigidbody2D>().gravityScale = Random.Range(5, 10);
        }
        spawned = true;
    }

    private bool FindByTag(string tag)
    {
        try
        {
            GameObject[] gm;
            gm=GameObject.FindGameObjectsWithTag(tag);
            if (gm.Length > 0)
            {
                Debug.Log("tru");
                return true;
            }
                

            else
            {
                Debug.Log("fals");
                return false;
            }
                
        }
        catch
        {
            Debug.Log("NoTunnel");
            return false;
        }
    }
   /* IEnumerator SpawnCounter()
    {
        Debug.Log("spawnCounter");
        changeSpawnCounter = false;
        float counterSeconds=0;
        counterSeconds = Random.Range(1, 3);
        yield return new WaitForSeconds(counterSeconds);
        Debug.Log("afterCounter");
        if (spawnBasic == true)
        {
            Debug.Log("spawnTunnel=true");
            spawnBasic = false;
            spawnTunnel = true;
        }
       /* else if (spawnTunnel == true)
        {
            Debug.Log("spawnBasic=true");
            spawnTunnel = false;
            spawnBasic = true;
        }
        changeSpawnCounter = true;
    }*/
}
