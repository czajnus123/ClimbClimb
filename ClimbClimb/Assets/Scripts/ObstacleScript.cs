using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

    private float speed=1f;
    bool spawned, switched;
    int leftCount, rightCount,level;
    PointManagerScript pointManager;

	// Use this for initialization
	void Start () {
        spawned = false;
        switched = false;
        leftCount = 0;
        rightCount = 0;
        pointManager = GameObject.Find("PointManager").GetComponent<PointManagerScript>();

        level = (int)(pointManager.GetPoints()/10)/2;
        try
        {
            if (gameObject.tag == "Obstacle")
            {
                GameControllerScript.Instance.speed = 5f;
                for (int i = 0; i < gameObject.transform.childCount - 1; i++)
                {
                    gameObject.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = GameControllerScript.Instance.BasicObSkins[level];
                }
            }
            else if (gameObject.tag == "Tunnel")
            {
                GameControllerScript.Instance.speed = 4f;
                if (pointManager.GetPoints() > 10)
                {
                    for (int i = 0; i < gameObject.transform.childCount - 1; i++)
                    {
                        gameObject.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = GameControllerScript.Instance.TunnelObSkins[level];
                    }
                }

            }
            else if (gameObject.tag == "ObstacleM")
            {
                GameControllerScript.Instance.speed = 5f;
                for (int i = 0; i < gameObject.transform.childCount - 1; i++)
                {
                    gameObject.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = GameControllerScript.Instance.MidObSkins[level];
                }
            }
        

        else if (gameObject.tag == "Coin") speed = 2f;
        }
        catch { }

        GameControllerScript.Instance.speed += (float) (.05f* GameObject.Find("PointManager").GetComponent<PointManagerScript>().GetPoints());
        if (GameControllerScript.Instance.speed > 20) GameControllerScript.Instance.speed = 20;

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
            if(gameObject.tag!="Coin")
            transform.Translate(Vector3.down * GameControllerScript.Instance.speed * Time.deltaTime);
            else
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
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
                if (gameObject.tag == "ObstacleM" && gameObject.transform.position.y <= GameObject.Find("spawnTriggerT").transform.position.y)
                {
                    GameControllerScript.Instance.toSpawn = true;
                    spawned = true;
                }
            }
            if (gameObject.tag == "ObstacleM" && gameObject.transform.position.y <= GameObject.Find("spawnTriggerT").transform.position.y&&switched==false)
            {
                var value = Random.RandomRange(0f, 2f);
                if (GameControllerScript.Instance.midLeft > 2)
                {
                    value = 2;
                    GameControllerScript.Instance.midLeft = 0;

                }
                if (GameControllerScript.Instance.midRight > 2)
                {
                    value = 0;
                    GameControllerScript.Instance.midRight = 0;

                }
                if (value < 1)
                {
                    gameObject.transform.position = Vector2.MoveTowards(new Vector2(gameObject.transform.position.x,gameObject.transform.position.y), 
                        new Vector2(GameObject.Find("spawnLeft").transform.position.x+(gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x/2)/2
                        ,gameObject.transform.position.y), 2);
                    GameControllerScript.Instance.midLeft = GameControllerScript.Instance.midLeft + 1;
                }
                else
                {
                    gameObject.transform.position = Vector2.MoveTowards(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),
                        new Vector2(GameObject.Find("spawnRight").transform.position.x- (gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x / 2)/2
                        , gameObject.transform.position.y), 2);
                    GameControllerScript.Instance.midRight = GameControllerScript.Instance.midRight + 1;
                }
                switched = true;
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
