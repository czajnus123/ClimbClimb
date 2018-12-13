using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour {

    private int type;

    private GameObject stars1;
    private GameObject stars2;
    private GameObject stars3;
    private GameControllerScript gameController;
    private Vector2 startPosition;

    // Use this for initialization
    void Start () {

        stars1 = GameObject.Find("Stars");
        stars2 = GameObject.Find("Stars2");
        stars3 = GameObject.Find("Stars3");
        gameController = GameObject.Find("mainObject").GetComponent<GameControllerScript>();
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
                    stars1.GetComponent<SpriteRenderer>().bounds.size.y));
                break;
            case 3:
                gameObject.transform.position = new Vector2(stars1.transform.position.x, (stars1.transform.position.y +
                    stars1.GetComponent<SpriteRenderer>().bounds.size.y*2));
                break;
        }
		
	}
	
	// Update is called once per frame
	void Update () {

        if (gameController.GetGameOver() == false)
        {
            transform.Translate(Vector3.down * .2f * Time.deltaTime);

            if (gameObject.transform.position.y <= startPosition.y - stars1.GetComponent<SpriteRenderer>().bounds.size.y)
            {
                switch (type)
                {
                    case 1:
                        gameObject.transform.position = new Vector2(stars3.transform.position.x, stars3.transform.position.y
                            + stars3.GetComponent<SpriteRenderer>().bounds.size.y);
                        break;
                    case 2:
                        gameObject.transform.position = new Vector2(stars1.transform.position.x, stars1.transform.position.y
                            + stars1.GetComponent<SpriteRenderer>().bounds.size.y);
                        break;
                    case 3:
                        gameObject.transform.position = new Vector2(stars2.transform.position.x, stars2.transform.position.y
                            + stars2.GetComponent<SpriteRenderer>().bounds.size.y);
                        break;
                }
            }
        }
		
	}
}
