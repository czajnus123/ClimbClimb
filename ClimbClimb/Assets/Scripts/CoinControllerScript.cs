using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControllerScript : MonoBehaviour {

    private GameControllerScript gameController;

	// Use this for initialization
	void Start () {

        gameController = GameObject.Find("mainObject").GetComponent<GameControllerScript>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameController.AddCoin(1);
            Destroy(gameObject);
        }
    }
}
