using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelObstacleController : MonoBehaviour {

    public GameObject direction;
    private GameControllerScript gameController;

    public float speed;

	// Use this for initialization
	void Start () {

        speed = 1;
        gameController = GameObject.Find("mainObject").GetComponent<GameControllerScript>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (gameController.GetGameOver() == false)
        {
            transform.Translate(Vector3.down * 4 * Time.deltaTime);
        }
        else
        {
            try
            {
                Destroy(transform.parent.gameObject);
            }
            catch
            {
                Destroy(gameObject);
            }

        }

    }


}
