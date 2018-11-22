using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControllerScript : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject oponentPrefab;
    public GameObject spawnPlayerPos;

    public bool rightSide;
    public bool gameOver;
    public bool endMenu;

    public float skin;

   // bool spawned;
    float seconds;
    int side;

	// Use this for initialization
	void Start () {
        rightSide = true;
        gameOver = true;
        endMenu = false;

        Instantiate(playerPrefab, new Vector2(spawnPlayerPos.transform.position.x, spawnPlayerPos.transform.position.y), Quaternion.identity);
        Instantiate(oponentPrefab, new Vector2(spawnPlayerPos.transform.position.x, Random.RandomRange(10,20)), Quaternion.identity);

        Application.targetFrameRate = 300;
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
}
