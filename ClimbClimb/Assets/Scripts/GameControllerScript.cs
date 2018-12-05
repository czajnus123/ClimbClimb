using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControllerScript : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject oponentPrefab;
    public GameObject spawnPlayerPos;
    public GameObject posLeft;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject spawnObstacleRight;
    public GameObject spawnObstacleLeft;
    public GameObject spawnLightLeft;
    public GameObject spawnLightRight;
    public GameObject background;

    private Color targetColor;
    float timeLeft;

    public bool rightSide;
    public bool gameOver;
    public bool endMenu;

    private int skin =0;
    public int Skin
    {
        get { return skin; }
        set { skin = value; }
    }

    private int coinCount;
    private int spawnCoinCounter;
    private int deathCount;

   // bool spawned;
    float seconds;
    int side;

	// Use this for initialization
	void Start () {
        //PlayerPrefs.SetInt("coins", 0);
        //PlayerPrefs.SetInt("position", 1000);
        //PlayerPrefs.SetFloat("highscore",0);
        rightSide = true;
        gameOver = true;
        endMenu = false;
        coinCount = 0;
        deathCount = 0;


        targetColor = background.GetComponent<SpriteRenderer>().material.color;

        posLeft.transform.position = new Vector2((leftWall.transform.position.x + leftWall.GetComponent<SpriteRenderer>().bounds.size.x/2
            + playerPrefab.GetComponent<SpriteRenderer>().bounds.size.x/2),posLeft.transform.position.y);

        spawnPlayerPos.transform.position= new Vector2((rightWall.transform.position.x - rightWall.GetComponent<SpriteRenderer>().bounds.size.x / 2
            - playerPrefab.GetComponent<SpriteRenderer>().bounds.size.x/2), spawnPlayerPos.transform.position.y);

        spawnObstacleLeft.transform.position = new Vector2(posLeft.transform.position.x, spawnObstacleLeft.transform.position.y);
        spawnObstacleRight.transform.position = new Vector2(spawnPlayerPos.transform.position.x, spawnObstacleRight.transform.position.y);
        spawnLightLeft.transform.position = new Vector2(posLeft.transform.position.x, spawnLightLeft.transform.position.y);
        spawnLightRight.transform.position = new Vector2(spawnPlayerPos.transform.position.x, spawnLightRight.transform.position.y);

        Instantiate(playerPrefab, new Vector2(spawnPlayerPos.transform.position.x, spawnPlayerPos.transform.position.y), Quaternion.identity);
        Instantiate(oponentPrefab, new Vector2(spawnPlayerPos.transform.position.x, Random.RandomRange(10,20)), Quaternion.identity);

       /* var player = GameObject.Find("Player(Clone)");
        GameObject.Find("LeftParticle").transform.position = new Vector2(GameObject.Find("posLeft").transform.position.x -
            player.GetComponent<SpriteRenderer>().bounds.size.x / 2, player.transform.position.y);

        GameObject.Find("RightParticle").transform.position = new Vector2(GameObject.Find("posRight").transform.position.x +
    player.GetComponent<SpriteRenderer>().bounds.size.x / 2, player.transform.position.y);*/

        Application.targetFrameRate = 600;
    }
	
	// Update is called once per frame
	void Update () {
            
            if (Input.touchCount > 0&&gameOver == true && endMenu == false)
            {
                gameOver = false;
                if (GameObject.Find("TapToPlay"))
                {
                    GameObject.Find("ShopButton").SetActive(false);
                    GameObject.Find("TapToPlay").SetActive(false);
                    GameObject.Find("NoAdsButton").SetActive(false);
                GameObject.Find("Oponent(Clone)").GetComponent<AIScript>().SetSpeed(0.5f);
                }
            }
        if (gameOver == false)
        {
            if (timeLeft <= Time.deltaTime)
            {
                background.GetComponent<SpriteRenderer>().material.color = targetColor;

                targetColor = new Color(Random.Range(0,3), Random.Range(0,3), Random.Range(0,3));

                timeLeft = 10.0f;
            }
            else
            {
                background.GetComponent<SpriteRenderer>().material.color = Color.Lerp(background.GetComponent<SpriteRenderer>().material.color, targetColor, Time.deltaTime / timeLeft);

                timeLeft -= Time.deltaTime;
            }
        }
		
	}

    public void SetGameOver(bool over)
    {
        gameOver = over;
    }

    public bool GetGameOver()
    {
        return gameOver;
    }

    public void SetEndMenu(bool endMenubool)
    {
        endMenu = endMenubool;
    }
    public bool GetEndMenuBool()
    {
 
        return endMenu;
    }
    public bool GetRightSideBool()
    {
        return rightSide;
    }
    public int GetCoinAmount()
    {
        return coinCount;
    }
    public void AddCoin(int coin)
    {
        coinCount += coin;
    }
    public int GetSpawnCoinCounter()
    {
        return spawnCoinCounter;
    }
    public void SetSpawnCoinCounter()
    {
        if (spawnCoinCounter > 9)
            spawnCoinCounter = 0;

        spawnCoinCounter++;
    }
    public void SetDeathCount()
    {
        deathCount ++;
        Debug.Log("deathCount: " + deathCount);
    }
    public int GetDeathCount()
    {
        return deathCount;
    }
}
