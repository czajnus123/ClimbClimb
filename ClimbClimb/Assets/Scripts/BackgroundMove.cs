using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour {

    private int type;

    private GameObject stars1, stars2,stars3;
    public GameObject upWallEdge;
    private Vector2 startPosition;
    private float dev;

    private int previousLevel;
    private bool changeBg;

    GameControllerScript gameController;

    // Use this for initialization
    void Start () {

        gameController = GameControllerScript.Instance;

        previousLevel = 0;
        changeBg = false;

        dev = 1f;

        stars1 = GameObject.Find("Stars");
        stars2 = GameObject.Find("Stars2");
        stars3 = GameObject.Find("Stars3");
        startPosition = stars1.transform.position;

        type = 0;

        if (gameObject.name == "Stars")
            type = 1;
        else if (gameObject.name == "Stars2")
            type = 2;
        else if (gameObject.name == "Stars3")
            type = 3;

        switch (type)
        {
            case 1:
                break;
            case 2:
                gameObject.transform.position = new Vector2(stars1.transform.position.x, (stars1.transform.position.y +
                    stars1.GetComponent<SpriteRenderer>().bounds.size.y/ dev));
                break;
            case 3:
                gameObject.transform.position = new Vector2(stars1.transform.position.x, (stars1.transform.position.y +
                    stars1.GetComponent<SpriteRenderer>().bounds.size.y/ dev*2));
                break;
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        if (GameControllerScript.Instance.gameOver== false)
        {

           /* if (previousLevel + 1 == gameController.level)
            {
                changeBg = true;
                previousLevel = gameController.level;
            }

            if (changeBg == true)
            {
                var pos = gameObject.transform.position;
                if (pos.y - stars1.GetComponent<SpriteRenderer>().bounds.size.y/2 >= upWallEdge.transform.position.y / 2)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = gameController.BackgroundSkins[gameController.level];
                    changeBg = false;
                }

            }*/



            transform.Translate(Vector3.down * gameController.speed * Time.deltaTime);

            if (gameObject.transform.position.y <= startPosition.y - stars1.GetComponent<SpriteRenderer>().bounds.size.y)
            {
                switch (type)
                {
                    case 1:
                        gameObject.transform.position = new Vector2(stars3.transform.position.x, stars3.transform.position.y
                            + stars3.GetComponent<SpriteRenderer>().bounds.size.y/ dev);
                        break;
                    case 2:
                        gameObject.transform.position = new Vector2(stars1.transform.position.x, stars1.transform.position.y
                            + stars1.GetComponent<SpriteRenderer>().bounds.size.y/ dev);
                        break;
                    case 3:
                        gameObject.transform.position = new Vector2(stars2.transform.position.x, stars2.transform.position.y
                            + stars2.GetComponent<SpriteRenderer>().bounds.size.y/ dev);
                        break;
                }
            }
        }
		
	}
}
