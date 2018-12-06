﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour {

    private int obstacleType;
    private float seconds;
    private float coinSeconds;
    private float points;
    private float side;

    private bool toSpawn;
    private bool toSpawnCoin;
    private bool changeSpawnCounter;
    private bool spawnBasic;
    private bool spawnTunnel;

    private int leftCount;
    private int rightCount;


    public GameObject spawnRight;
    public GameObject spawnLeft;
    public GameObject spawnMid;
    public GameObject basicObstacle;
    public GameObject tunnelRightObstacle;
    public GameObject tunnelLeftObstacle;
    public GameObject coin;
    public GameObject spawnLightLeftPos;
    public GameObject spawnLightRightPos;
    public GameObject lightningPrefab;

    private GameControllerScript gameController;
    private PointManagerScript pointManager;
    private PlayerScript playerScript;
    


	// Use this for initialization
	void Start () {

        points = 0;
        leftCount = 0;
        rightCount = 0;

        gameController = GameObject.Find("mainObject").GetComponent<GameControllerScript>();
        pointManager = GameObject.Find("PointManager").GetComponent<PointManagerScript>();

        toSpawn = true;
        toSpawnCoin = true;
        spawnBasic = true;
        spawnTunnel = false;
        changeSpawnCounter = true;		
	}
	
	// Update is called once per frame
	void Update () {

        if (gameController.GetGameOver() == false)
        {
            if (gameController.GetToSpawn() == true)
            {
                switch (obstacleType)
                {
                    case 0:
                        if (!FindByTag("Tunnel"))
                        {
                            SpawnBasic();
                        }
                        break;
                    case 1:
                        SpawnTunnel();
                        break;
                        
                }
                /*if (!FindByTag("Tunnel") && !FindByTag("Obstacle"))
                    {
                        obstacleType = 0;
                    //StartCoroutine("SpawnLightning");
                    }
                }*/
            }
            if (toSpawnCoin == true)
            {
                StartCoroutine("SpawnCoin");
            }
            if (pointManager.GetPoints() >= points + 10)
            {
                points = pointManager.GetPoints();
                var ob = Random.Range(0f, 2f);
                obstacleType = (int)ob;
            }
        }
    }

    void SpawnBasic()
    {
        gameController.SetToSpawn(false);
        side = Mathf.Round(Random.RandomRange(0f, 1f));
        if (leftCount >2)
        {
            side = 1;
            leftCount = 0;

        }
        if (rightCount > 2)
        {
            side = 0;
            rightCount = 0;

        }
        if (side == 0)
        {
            leftCount = leftCount + 1;
            Instantiate(basicObstacle, new Vector2(spawnLeft.transform.position.x, spawnLeft.transform.position.y), Quaternion.identity);
        }
        else
        {
            rightCount = rightCount + 1;
            Instantiate(basicObstacle, new Vector2(spawnRight.transform.position.x, spawnRight.transform.position.y), Quaternion.identity);
        }

    }

    void SpawnTunnel()
    {
        gameController.SetToSpawn(false);
        side = Mathf.Round(Random.RandomRange(0f, 1f));

        if (side == 0)
        {
            Instantiate(tunnelLeftObstacle, new Vector2(spawnMid.transform.position.x, spawnMid.transform.position.y), Quaternion.identity);
        }
        else
        {
            Instantiate(tunnelRightObstacle, new Vector2(spawnMid.transform.position.x, spawnMid.transform.position.y), Quaternion.identity);
        }
    }

    IEnumerator SpawnCoin()
    {
        toSpawnCoin = false;
        coinSeconds = Random.RandomRange(10, 20);
        yield return new WaitForSeconds(coinSeconds);
        Instantiate(coin, new Vector2(spawnMid.transform.position.x, spawnMid.transform.position.y), Quaternion.identity);
        toSpawnCoin = true;
    }

   /* IEnumerator SpawnLightning()
    {
        toSpawn = false;
        seconds = Random.RandomRange(.8f, 1.5f);
        yield return new WaitForSeconds(seconds);
        if (gameController.GetRightSideBool() == true)
        {
            Instantiate(lightningPrefab, new Vector2(spawnLightRightPos.transform.position.x, spawnLightRightPos.transform.position.y), Quaternion.identity);
        }
        else
        {
            Instantiate(lightningPrefab, new Vector2(spawnLightLeftPos.transform.position.x, spawnLightLeftPos.transform.position.y), Quaternion.identity);
        }
        toSpawn = true;


    }*/

    private bool FindByTag(string tag)
    {
        try
        {
            GameObject[] gm;
            gm=GameObject.FindGameObjectsWithTag(tag);
            if (gm.Length > 0)
            {
                return true;
            }
            else
            {
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
