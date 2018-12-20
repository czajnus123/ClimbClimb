using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsMove : MonoBehaviour {

    private int type;

    private GameObject walls1, walls2, walls3;
    private float wallSprite,wallSprite2,wallSprite3;
    private Vector2 startPosition,TwoPosition,ThreePosition;

    // Use this for initialization
    void Start()
    {

        walls1 = GameObject.Find("Walls1");
        walls2 = GameObject.Find("Walls2");
        walls3 = GameObject.Find("Walls3");
        wallSprite = walls1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        wallSprite2 = walls2.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        wallSprite3 = walls3.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        startPosition = walls1.transform.position;


        type = 0;

        if (gameObject.name == "Walls1")
            type = 1;
        else if (gameObject.name == "Walls2")
            type = 2;
        else if (gameObject.name == "Walls3")
            type = 3;

        switch (type)
        {
            case 1:
                break;
            case 2:
                gameObject.transform.position = new Vector2(walls1.transform.position.x, (walls1.transform.position.y +
                   wallSprite));
                break;
            case 3:
                gameObject.transform.position = new Vector2(walls1.transform.position.x, (walls1.transform.position.y +
                    wallSprite2+wallSprite));
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (GameControllerScript.Instance.gameOver == false)
        {
            transform.Translate(Vector3.down * GameControllerScript.Instance.speed * Time.deltaTime);

            
                switch (type)
                {
                    case 1:
                    if (gameObject.transform.position.y <= startPosition.y - walls1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().bounds.size.y)
                    {
                        gameObject.transform.position = new Vector2(walls3.transform.position.x,walls3.transform.position.y+
                            wallSprite3);
                    }
                        break;
                    case 2:
                    if (gameObject.transform.position.y <= startPosition.y - walls2.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().bounds.size.y)
                    {
                        gameObject.transform.position = new Vector2(walls1.transform.position.x, walls1.transform.position.y +
                            wallSprite);
                    }
                        break;
                    case 3:
                    if (gameObject.transform.position.y <= startPosition.y - walls3.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().bounds.size.y)
                    {
                        gameObject.transform.position = new Vector2(walls2.transform.position.x, walls2.transform.position.y +
                            wallSprite2-.2f);
                    }
                        break;
                }
            
        }

    }
}
