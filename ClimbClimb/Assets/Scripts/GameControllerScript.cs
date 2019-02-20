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

    public GameObject playerPrefab, oponentPrefab, spawnPlayerPos, posLeft, leftWall, rightWall, spawnObstacleRight, spawnObstacleLeft,
        spawnLightLeft, spawnLightRight, background, background2, content,player,obstaclePrefab, rain, rainSpawnWayp,texts;

    private PointManagerScript pointManager;

    public Sprite[] skins,BasicObSkins,MidObSkins,TunnelObSkins,LeftWalLSkins,RightWallSkins,BackgroundSkins;
    public GameObject[] PlayerExplosions, PlayerTrails, Rains,lasers;
    public Button basicButton;
    private Color targetColor;
    public Color[] shopPlatformColor, shopLightColor;

    public bool rightSide, gameOver, endMenu, toSpawn, shopActive,check, laserShrink, resumeRain;

    public int coinCount, spawnCoinCounter, deathCount, currentSkinIndex = 0, skinAvailability = 0, midLeft = 0, midRight = 0, level = 0;

    private int skin = 0, side, previousLevel;

    public float speed;

    private float timeLeft, seconds;

    private bool changeBg;

    public int Skin
    {
        get { return skin; }
        set { skin = value; }
    }

	void Awake () {
        shopActive = false;
        rightSide = true;
        gameOver = true;
        endMenu = false;
        toSpawn = true;
        check = true;
        coinCount = 0;
        deathCount = 0;
        currentSkinIndex = 0;
        instance = this;
        speed = 5;
        laserShrink = false;
        level = 0;
        previousLevel = -1;
        resumeRain = false;
        changeBg = false;

        var dev = 5f;

        targetColor = background.GetComponent<SpriteRenderer>().material.color;

        posLeft.transform.position = new Vector2((leftWall.transform.position.x + leftWall.GetComponent<SpriteRenderer>().bounds.size.x/dev
            + playerPrefab.GetComponent<SpriteRenderer>().bounds.size.x/5),posLeft.transform.position.y);

        spawnPlayerPos.transform.position= new Vector2((rightWall.transform.position.x - rightWall.GetComponent<SpriteRenderer>().bounds.size.x / dev
            - playerPrefab.GetComponent<SpriteRenderer>().bounds.size.x/5), spawnPlayerPos.transform.position.y);

        spawnObstacleLeft.transform.position = new Vector2((leftWall.transform.position.x + leftWall.GetComponent<SpriteRenderer>().bounds.size.x / dev
            + obstaclePrefab.GetComponent<SpriteRenderer>().bounds.size.x / 4), spawnObstacleLeft.transform.position.y);
        spawnObstacleRight.transform.position = new Vector2((rightWall.transform.position.x - rightWall.GetComponent<SpriteRenderer>().bounds.size.x / (dev-.2f)
            - obstaclePrefab.GetComponent<SpriteRenderer>().bounds.size.x / 4), spawnObstacleLeft.transform.position.y);
        spawnLightLeft.transform.position = new Vector2(posLeft.transform.position.x, spawnLightLeft.transform.position.y);
        spawnLightRight.transform.position = new Vector2(spawnPlayerPos.transform.position.x, spawnLightRight.transform.position.y);

        Instantiate(playerPrefab, new Vector2(spawnPlayerPos.transform.position.x, spawnPlayerPos.transform.position.y), Quaternion.identity);
        Instantiate(oponentPrefab, new Vector2(spawnPlayerPos.transform.position.x, Random.RandomRange(10,20)), Quaternion.identity);


        player = GameObject.Find("Player(Clone)");

        pointManager = GameObject.Find("PointManager").GetComponent<PointManagerScript>();

        Load();
        SpawnSkins();
        ChangeSkin(currentSkinIndex);

        Application.targetFrameRate = 600;

    }
	
	void Update ()
    {

        

        level = (int)(pointManager.GetPoints() / 10) / 2;

        coinText.text = PlayerPrefs.GetInt("Coins").ToString();
            
            if (Input.touchCount > 0 && gameOver == true && endMenu == false && Input.touches[0].phase==TouchPhase.Began&&shopActive==false)
            {
            if (!EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
            {
                gameOver = false;
                FindObjectOfType<AudioManagerScript>().Play("Theme");
                if (GameObject.Find("TapToPlay"))
                {
                    GameObject.Find("ShopButton").SetActive(false);
                    GameObject.Find("TapToPlay").SetActive(false);
                    GameObject.Find("LOGO").SetActive(false);
                    texts.SetActive(true);
                    
                   // GameObject.Find("NoAdsButton").SetActive(false);
                    GameObject.Find("Oponent(Clone)").GetComponent<AIScript>().SetSpeed(0.5f);
                }
            }
            }
        if (gameOver == false)
        {
            if (resumeRain == true)
            {
                rain.GetComponent<ParticleSystem>().Play();
                resumeRain = false;
            }
            if (previousLevel == -1)
            {
                rain = Instantiate(Rains[level], rainSpawnWayp.transform.position, Quaternion.identity);
                previousLevel = 0;
            }

            if (previousLevel + 1 == level)
            {
                var pr = rain;
                StartCoroutine(DestroyPreviousRain(pr));
                rain = Instantiate(Rains[level], rainSpawnWayp.transform.position, Quaternion.identity);
                previousLevel = level;
                //leftWall.GetComponent<SpriteRenderer>().sprite = LeftWalLSkins[level];

                // rightWall.GetComponent<SpriteRenderer>().sprite = RightWallSkins[level];
               /* background.GetComponent<SpriteRenderer>().sprite = BackgroundSkins[level];
                for (int i = 0; i < background.transform.childCount; i++)
                {
                    background.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = BackgroundSkins[level];
                }*/

            }


            speed += .05f * Time.deltaTime;
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

        if (gameOver == true)
        {
            try
            {
                rain.GetComponent<ParticleSystem>().Pause();
            }
            catch
            {

            }

        }

        if (check == true)
        {
            SetPlayerMovingEffect();
            check = false;
        }
		
	}
    public void SetPlayerMovingEffect()
    {
       
        Destroy(player.transform.GetChild(2).gameObject);

        Vector2 vec = player.transform.position;
        GameObject go = Instantiate(PlayerTrails[currentSkinIndex], vec, Quaternion.identity);
        go.transform.parent = player.transform;
    }

    public void AddCoin(int coin)
    {
        coinCount += coin;
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

    IEnumerator DestroyPreviousRain(GameObject previousRain)
    {
        var ps = previousRain.GetComponent<ParticleSystem>().emission;
        ps.rateOverTime = 0;
        yield return new WaitForSeconds(10f);
        Destroy(previousRain);
    }
    public void ResetSaves()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("SkinAvailability", 1);
        PlayerPrefs.SetInt("CurrentSkin", 0);
        PlayerPrefs.SetInt("Coins", 0);
    }
}
