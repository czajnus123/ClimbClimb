using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class GameControllerScript : MonoBehaviour {

    private static GameControllerScript instance;
    public static GameControllerScript Instance { get { return instance; } }

    public TextMeshProUGUI coinText;

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
    public GameObject background2;
    public GameObject content;
    public Sprite[] skins;
    public Button basicButton;

    private Color targetColor;
    float timeLeft;

    public bool rightSide;
    public bool gameOver;
    public bool endMenu;
    public bool toSpawn;
    public bool shopActive;

    private int skin =0;
    public int Skin
    {
        get { return skin; }
        set { skin = value; }
    }

    public int coinCount;
    private int spawnCoinCounter;
    private int deathCount;

    public int currentSkinIndex = 0;
    public int skinAvailability = 0;

   // bool spawned;
    float seconds;
    int side;

	// Use this for initialization
	void Awake () {
        //PlayerPrefs.SetInt("coins", 0);
        //PlayerPrefs.SetInt("position", 1000);
        //PlayerPrefs.SetFloat("highscore",0);
        shopActive = false;
        rightSide = true;
        gameOver = true;
        endMenu = false;
        toSpawn = true;
        coinCount = 0;
        deathCount = 0;
        currentSkinIndex = 0;
        instance = this;

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

       // ResetSaves();
        Load();
        SpawnSkins();
        ChangeSkin(currentSkinIndex);

        Application.targetFrameRate = 600;
    }
	
	// Update is called once per frame
	void Update () {
        coinText.text = PlayerPrefs.GetInt("Coins").ToString();
            
            if (Input.touchCount > 0 && gameOver == true && endMenu == false && Input.touches[0].phase==TouchPhase.Began&&shopActive==false)
            {
            if (!EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
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
            }
        if (gameOver == false)
        {
            if (timeLeft <= Time.deltaTime)
            {
                background.GetComponent<SpriteRenderer>().material.color = targetColor;
                background2.GetComponent<SpriteRenderer>().material.color = targetColor;

                targetColor = new Color(Random.Range(0,3), Random.Range(0,3), Random.Range(0,3));

                timeLeft = 10.0f;
            }
            else
            {
                background.GetComponent<SpriteRenderer>().material.color = Color.Lerp(background.GetComponent<SpriteRenderer>().material.color, targetColor, Time.deltaTime / timeLeft);
                background2.GetComponent<SpriteRenderer>().material.color = Color.Lerp(background.GetComponent<SpriteRenderer>().material.color, targetColor, Time.deltaTime / timeLeft);

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

    public bool GetToSpawn()
    {
        return toSpawn;
    }

    public void SetToSpawn(bool spawn)
    {
        toSpawn = spawn;
    }
    public void SetShopState(bool active)
    {
        shopActive = active;

    }

    private void ChangeSkin(int index)
    {
        GameObject.Find("Player(Clone)").GetComponent<SpriteRenderer>().sprite = skins[index];


    }
    public void SetCurrentSkinIndex(int index)
    {
        currentSkinIndex = index;
    }

    public void SpawnSkins()
    {
        for (int i = 0; i < skins.Length; i++)
        {
            var skin = Instantiate(basicButton, Vector3.zero, Quaternion.identity);
            skin.transform.parent = content.transform;
            skin.GetComponent<Image>().sprite = skins[i];
            skin.transform.localScale = new Vector2(2, 2);
            if ((skinAvailability & 1 << i) == 1 << i)
            {
                skin.transform.GetChild(0).gameObject.SetActive(false);
            }
            var skin2 = skins[i];
        }
        

    }

    public void Save()
    {
        PlayerPrefs.SetInt("SkinAvailability", skinAvailability);
        PlayerPrefs.SetInt("CurrentSkin", currentSkinIndex);
        PlayerPrefs.SetInt("Coins", coinCount);
    }
    public void Load()
    {
        if (PlayerPrefs.HasKey("CurrentSkin"))
        {
            skinAvailability = PlayerPrefs.GetInt("SkinAvailability");
            currentSkinIndex = PlayerPrefs.GetInt("CurrentSkin");
            coinCount = PlayerPrefs.GetInt("Coins");
            coinText.text = coinCount.ToString();
        }
        else
        {
            Debug.Log("first play");
            PlayerPrefs.SetInt("SkinAvailability", 1);
            PlayerPrefs.SetInt("CurrentSkin", 0);
            PlayerPrefs.SetInt("Coins", 0);
            PlayerPrefs.SetInt("position", 1000);
            PlayerPrefs.SetFloat("highscore", 0);
            Load();
        }
    }
    public void ResetSaves()
    {
        PlayerPrefs.DeleteAll();
    }
}
