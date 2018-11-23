using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private GameObject posRight;
    private GameObject posLeft;
    private GameControllerScript gameController;
    private SaveScript saveManager;
    private Touch touch;

    private bool rightSide;
    private bool left;
    private bool leftSlam;
    private bool rightSlam;
    bool tapped;

	// Use this for initialization
	void Start () {
        rightSide = GameObject.Find("mainObject").GetComponent<GameControllerScript>().rightSide;
        gameController = GameObject.Find("mainObject").GetComponent<GameControllerScript>();
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveScript>();
        left = false;
        tapped = false;
        leftSlam = false;
        rightSlam = false;

        posRight = GameObject.Find("posRight");
        posLeft = GameObject.Find("posLeft");

		
	}
	
	// Update is called once per frame
	void Update () {

        if (gameController.GetGameOver() == false)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
            }

                 
                if (touch.phase == TouchPhase.Began)
                {
                    tapped = true;
                }
                if (tapped == true)
                {
                    if (rightSide == true)
                    {
                    gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, posLeft.transform.position, 2);
                    
                    if (gameObject.transform.position.x == posLeft.transform.position.x)
                    {
                        gameObject.transform.Find("LeftSlam").gameObject.SetActive(true);
                        Debug.Log("LeftSlam");
                        if (leftSlam == false)
                        {
                            StartCoroutine("LeftSlamCounter");
                        }
                    }

                    GameObject.Find("mainObject").GetComponent<GameControllerScript>().rightSide = false;
                    }
                    else if (rightSide == false)
                    {
                    gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, posRight.transform.position, 2);
                    
                    if (gameObject.transform.position.x == posRight.transform.position.x)
                    {
                        gameObject.transform.Find("RightSlam").gameObject.SetActive(true);
                        if (rightSlam == false)
                        {
                            StartCoroutine("RightSlamCounter");
                        }
                    }

                    GameObject.Find("mainObject").GetComponent<GameControllerScript>().rightSide = true;
                    }
                    if (gameObject.transform.position == posLeft.transform.position)
                    {
                        tapped = false;
                        rightSide = false;
                    }
                    else if (gameObject.transform.position == posRight.transform.position)
                    {
                        tapped = false;
                        rightSide = true;
                    }
                }
            }

        
		
	}

    IEnumerator LeftSlamCounter()
    {
        leftSlam = true;
        yield return new WaitForSeconds(.1f);
        gameObject.transform.Find("LeftSlam").gameObject.SetActive(false);
        leftSlam = false;

    }

    IEnumerator RightSlamCounter()
    {
        rightSlam = true;
        yield return new WaitForSeconds(.1f);
        gameObject.transform.Find("RightSlam").gameObject.SetActive(false);
        rightSlam = false;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
            GameObject.Find("Canvas").transform.Find("Panel").gameObject.SetActive(true);
            gameController.SetGameOver(true);
            gameController.SetEndMenu(true);
            saveManager.Save();
            try
            {
                Destroy(collision.transform.parent.gameObject);
            }
            catch
            {
                Destroy(collision.gameObject);
            }

            Destroy(gameObject);

    }
}
