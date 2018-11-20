using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private GameObject posRight;
    private GameObject posLeft;
   // private GameObject gameController;
    private GameControllerScript gameController;

    private bool rightSide;
    private bool left;
    bool tapped;

	// Use this for initialization
	void Start () {
        rightSide = GameObject.Find("mainObject").GetComponent<GameControllerScript>().rightSide;
        gameController = GameObject.Find("mainObject").GetComponent<GameControllerScript>();
        left = false;
        tapped = false;

        posRight = GameObject.Find("posRight");
        posLeft = GameObject.Find("posLeft");

		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.touchCount > 0)
        {
            if (gameController.GetGameOver() == false)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    tapped = true;
                }
                if (tapped == true)
                {
                    if (rightSide == true)
                    {
                        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, posLeft.transform.position, 2);
                        GameObject.Find("mainObject").GetComponent<GameControllerScript>().rightSide = false;
                    }
                    else if (rightSide == false)
                    {
                        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, posRight.transform.position, 2);

                        GameObject.Find("mainObject").GetComponent<GameControllerScript>().rightSide = true;
                    }
                    if (gameObject.transform.position.x == posLeft.transform.position.x)
                    {
                        tapped = false;
                        rightSide = false;
                    }
                    else if (gameObject.transform.position.x == posRight.transform.position.x)
                    {
                        tapped = false;
                        rightSide = true;
                    }
                }
            }

        }
		
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject.Find("Canvas").transform.Find("Panel").gameObject.SetActive(true);
        Destroy(gameObject);
        gameController.SetGameOver(true);


    }
}
