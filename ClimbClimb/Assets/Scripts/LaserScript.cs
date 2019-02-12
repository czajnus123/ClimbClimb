using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {
    private GameControllerScript gameoController;

    public GameObject straightLine, curveLine;


	// Use this for initialization
	void Start () {
        gameoController = GameControllerScript.Instance;
        StartCoroutine(ShootLaser());
	}
	
	// Update is called once per frame
	void Update () {

        if (gameoController.laserShrink == true)
        {
            straightLine.GetComponent<LineRenderer>().startWidth-=3f*Time.deltaTime;
            straightLine.GetComponent<LineRenderer>().endWidth -= 3f * Time.deltaTime;
            curveLine.GetComponent<LineRenderer>().startWidth -= 3f * Time.deltaTime;
            curveLine.GetComponent<LineRenderer>().endWidth -= 3f * Time.deltaTime;
        }
		
	}

    IEnumerator ShootLaser()
    {
        yield return new WaitForSeconds(.7f);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        gameoController.toSpawn = true;

    }
}
