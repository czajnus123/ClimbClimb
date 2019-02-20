using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour {

    private int obstacleType, leftCount, rightCount;
    private float seconds, coinSeconds, points, side, sideCam;

    private bool toSpawn, toSpawnCoin, changeSpawnCounter, spawnBasic, spawnTunnel, 
        camSwithced, zeroed = false, rotated = false, rotatingCamera;


    public GameObject spawnRight, spawnLeft, spawnMid, basicObstacle, tunnelRightObstacle, tunnelLeftObstacle, coin, spawnLightLeftPos,
        spawnLightRightPos, lightningPrefab, camera, obstacleMid,basicObstacleObj, laserObstacle, rightLaserSpawn,leftLaserSpawn, lastObstacleTrigger;

    private GameControllerScript gameController;
    private PointManagerScript pointManager;
    private PlayerScript playerScript;
    private GameObject lastObstacle;
    private string lastObstacleTag;

    


	// Use this for initialization
	void Start () {
        points = 0;
        leftCount = 0;
        rightCount = 0;

        gameController = GameControllerScript.Instance;
        pointManager = GameObject.Find("PointManager").GetComponent<PointManagerScript>();

        toSpawn = true;
        toSpawnCoin = true;
        spawnBasic = true;
        spawnTunnel = false;
        changeSpawnCounter = true;
        camSwithced = false;
        zeroed = false;
        rotated = false;
        rotatingCamera = false;
        obstacleType = 0;
        lastObstacle = lastObstacleTrigger;
        //lastObstacle.transform.position.y = lastObstacleTrigger.transform.position.y - 1f;
}
	
	// Update is called once per frame
	void Update () {

        if (gameController.gameOver == false)
        {
            if (gameController.toSpawn == true)
            {
                //Debug.Log("last obstacle name: " + lastObstacle.name);
                switch (obstacleType)
                {
                    case 0:
                        if (lastObstacleTag == "Tunnel")
                        {
                            if (CheckLastObstacle(lastObstacle))
                                SpawnBasic();
                        }
                        else
                        {
                            SpawnBasic();
                        }
                        
                        break;
                    case 1:
                        SpawnTunnel();
                        break;
                    case 2:
                        rotatingCamera = true;
                        if (lastObstacleTag == "Laser")
                            lastObstacleTag = "Obstacle";
                        if (lastObstacleTag != "Tunnel")
                            SpawnBasic();
                        
                        else if (CheckLastObstacle(lastObstacle))
                                SpawnBasic();
                        
                        
                        break;
                    case 3:
                        SpawnMiddleOb();
                        break;
                    case 4:
                        if (lastObstacleTag == "Tunnel" || lastObstacleTag == "Obstacle" || lastObstacleTag == "ObstacleM")
                        {
                            if (CheckLastObstacle(lastObstacle))
                                SpawnLaser();
                        }
                        else
                        {
                            SpawnLaser();
                        }
                        break;
                }
                lastObstacleTag = lastObstacle.tag;
            }
            if (toSpawnCoin == true)
            {
                StartCoroutine("SpawnCoin");
            }
            if (rotatingCamera == true)
            {
                RotateCamera();
            }
            if (pointManager.GetPoints() >= points + 10)
            {
                if (zeroed == false)
                {
                    if (camera.transform.rotation != Quaternion.Euler(0, 0, 0))
                    {
                        var target = Quaternion.Euler(0, 0, 0);
                        camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, target, 2 * Time.deltaTime);
                    }
                    else
                    {
                        zeroed = true;
                        sideCam = Random.RandomRange(0f, 2f);
                       // Debug.Log("sidecam:" + sideCam);
                    }
                }
                else
                {
                    points = pointManager.GetPoints();
                    var ob = Random.Range(0f, 5f);
                    obstacleType = (int)ob;
                    Debug.Log("Obstacle type: " + obstacleType);
                    //obstacleType =4;
                }
                
            }
        }
    }
    void SpawnMiddleOb()
    {
        gameController.toSpawn = false;
        lastObstacle=Instantiate(obstacleMid, new Vector2(spawnMid.transform.position.x, spawnMid.transform.position.y), Quaternion.identity);

    }

    bool CheckLastObstacle(GameObject lO)
    {
        
        if (obstacleType!=4&& lO.transform.position.y <= lastObstacleTrigger.transform.position.y)
        {
            return true;
        }
        else if (obstacleType == 4 && lO.transform.position.y <= GameObject.Find("lastObLaser").transform.position.y)
        {
            return true;
        }
        else
            return false;
    }

    void SpawnBasic()
    {
        gameController.toSpawn=false;
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
            Vector2[] leftVectorTab = {
                new Vector2(spawnLeft.transform.position.x + basicObstacleObj.GetComponent<SpriteRenderer>().bounds.size.x/1.1f, spawnLeft.transform.position.y),
                new Vector2(spawnLeft.transform.position.x, spawnLeft.transform.position.y+basicObstacleObj.GetComponent<SpriteRenderer>().bounds.size.y),
                                new Vector2(spawnLeft.transform.position.x, spawnLeft.transform.position.y-basicObstacleObj.GetComponent<SpriteRenderer>().bounds.size.y),
            };
            GameObject gp= Instantiate(basicObstacle, new Vector2(spawnLeft.transform.position.x, spawnLeft.transform.position.y), Quaternion.identity);
            GameObject go1= Instantiate(basicObstacleObj, new Vector2(spawnLeft.transform.position.x, spawnLeft.transform.position.y), Quaternion.identity);
            go1.transform.parent = gp.transform;
            int los = Random.RandomRange(1, leftVectorTab.Length+1); 
            for (int i = 0; i < los; i++)
            {
                GameObject go = Instantiate(basicObstacleObj, leftVectorTab[i], Quaternion.identity);
                if (i == 0)
                {
                    go.GetComponent<BoxCollider2D>().enabled = false;
                }
                go.tag = "ObstacleClone";
                go.transform.parent = gp.transform;
                
            }
            lastObstacle = gp;

        }
        else
        {
            rightCount = rightCount + 1;
            Vector2[] rightVectorTab = {
                new Vector2(spawnRight.transform.position.x -basicObstacleObj.GetComponent<SpriteRenderer>().bounds.size.x/1.1f, spawnLeft.transform.position.y),
                new Vector2(spawnRight.transform.position.x, spawnLeft.transform.position.y+basicObstacleObj.GetComponent<SpriteRenderer>().bounds.size.y),
                                new Vector2(spawnRight.transform.position.x, spawnLeft.transform.position.y-basicObstacleObj.GetComponent<SpriteRenderer>().bounds.size.y),
            };
            GameObject gp = Instantiate(basicObstacle, new Vector2(spawnRight.transform.position.x, spawnRight.transform.position.y), Quaternion.identity);
            GameObject go1 = Instantiate(basicObstacleObj, new Vector2(spawnRight.transform.position.x, spawnLeft.transform.position.y), Quaternion.identity);
            go1.transform.parent = gp.transform;
            int los = Random.RandomRange(1, rightVectorTab.Length + 1); //0 right, 1up,2down
            for (int i = 0; i < los; i++)
            {
                GameObject go = Instantiate(basicObstacleObj, rightVectorTab[i], Quaternion.identity);
                if (i == 0)
                {
                    go.GetComponent<BoxCollider2D>().enabled = false;
                }
                go.tag = "ObstacleClone";
                go.transform.parent = gp.transform;
            }
            lastObstacle = gp;
        }

    }

    void SpawnTunnel()
    {
        gameController.toSpawn=false;
        side = Mathf.Round(Random.RandomRange(0f, 1f));

        if (side == 0)
        {
            lastObstacle = Instantiate(tunnelLeftObstacle, new Vector2(spawnMid.transform.position.x, spawnMid.transform.position.y), Quaternion.identity);
        }
        else
        {
            lastObstacle = Instantiate(tunnelRightObstacle, new Vector2(spawnMid.transform.position.x, spawnMid.transform.position.y), Quaternion.identity);
        }
    }

    void SpawnLaser()
    {
        gameController.toSpawn = false;
        laserObstacle = gameController.lasers[gameController.level];
        side = Mathf.Round(Random.RandomRange(0f, 1f));
        if (side >= 0 && side < 1)
        {
            lastObstacle=Instantiate(laserObstacle, new Vector2(leftLaserSpawn.transform.position.x, leftLaserSpawn.transform.position.y), Quaternion.identity);
        }
        else
        {
            lastObstacle=Instantiate(laserObstacle, new Vector2(rightLaserSpawn.transform.position.x, rightLaserSpawn.transform.position.y), Quaternion.identity);

        }
    }

    void RotateCamera()
    {

        if (zeroed == true && rotated == false)
        {

            if (sideCam < 1)
            {
                if (camera.transform.rotation != Quaternion.Euler(0, 0, 45))
                {
                    var target = Quaternion.Euler(0, 0, 45);
                    camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, target, 2 * Time.deltaTime);
                }
                else
                    rotated = true;
            }
            else if (sideCam >= 1)
            {
                if (camera.transform.rotation != Quaternion.Euler(0, 0, -45))
                {
                    var target = Quaternion.Euler(0, 0, -45);
                    camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, target, 2 * Time.deltaTime);
                }
                else
                    rotated = true;
            }

        }
        if (rotated == true)
        {
            var obsType = Random.RandomRange(0f, 2f);
            if (obsType <= 1)
                obstacleType = 0;
            else
                obstacleType = 1;

            zeroed = false;
            rotated = false;
            rotatingCamera = false;
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
