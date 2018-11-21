using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

    private GameControllerScript gameController;
    private float speed=1f;
	// Use this for initialization
	void Start () {
        if (gameObject.tag == "Obstacle") speed = 7f;
        else if (gameObject.tag == "Tunnel") speed = 4f;
        gameController = GameObject.Find("mainObject").GetComponent<GameControllerScript>();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gameController.GetGameOver() == false)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
