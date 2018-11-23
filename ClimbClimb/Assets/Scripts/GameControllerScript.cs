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

    public bool rightSide;
    public bool gameOver;
    public bool endMenu;

    public float skin;

    private int coinCount;
    private int spawnCoinCounter;

   // bool spawned;
    float seconds;
    int side;

	// Use this for initialization
	void Start () {
        rightSide = true;
        gameOver = true;
        endMenu = false;
        coinCount = 0;

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

        Application.targetFrameRate = 600;
    }
	
	// Update is called once per frame
	void Update () {
            
            if (Input.touchCount > 0&&gameOver == true && endMenu == false)
            {
                gameOver = false;
                if (GameObject.Find("TapToPlay"))
                {
                    GameObject.Find("TapToPlay").SetActive(false);
                    GameObject.Find("Oponent(Clone)").GetComponent<AIScript>().SetSpeed(0.5f);
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
}
