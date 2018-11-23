using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour {

    private GameObject posRight;
    private GameObject posLeft;
    private GameControllerScript gameController;
    private GameObject player;

    private bool wyprzedzoned = false;

    private float speed = 0;
    public void SetSpeed(float value)
    {
        this.speed = value;
    }

	// Use this for initialization
	void Start () {
        posRight = GameObject.Find("posRight");
        posLeft = GameObject.Find("posLeft");
        gameController = GameObject.Find("mainObject").GetComponent<GameControllerScript>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (gameController.GetGameOver() == false)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (gameObject.transform.position.y < player.GetComponent<Transform>().transform.position.y && !wyprzedzoned)
            {
                wyprzedzoned = true;
                PlayerPrefs.SetInt("position", PlayerPrefs.GetInt("position", 1000) - 1);
            }
        }
        else if(gameController.GetGameOver() && speed>0)
        {
            Destroy(gameObject);
        }
        
    }

    public void change_side()
    {
        if (this.transform.position.x > 0)
            while(this.transform.position.x != posLeft.transform.position.x)
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position,
                    new Vector2(posLeft.transform.position.x, gameObject.transform.position.y), 2);
        else
            while (this.transform.position.x != posRight.transform.position.x)
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position,
                    new Vector2(posRight.transform.position.x, gameObject.transform.position.y), 2);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            change_side();
        if (collision.gameObject.name == "End")
            Destroy(gameObject);
    }
}
