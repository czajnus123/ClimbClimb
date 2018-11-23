using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningObstacleScript : MonoBehaviour {

    private GameControllerScript gameController;
    private Vector2 centre;

    private float rotateSpeed = 200f;
    private float radius = 0.2f;
    private float angle;

    private bool rotating;
    private bool beam;

    public float speed;


    // Use this for initialization
    void Start () {

        rotating = true;
        beam = false;
        speed = 50f;
        gameController = GameObject.Find("mainObject").GetComponent<GameControllerScript>();
        centre = transform.position;

    }

    // Update is called once per frame
    void Update () {

        if (gameController.GetGameOver() == false)
        {
            if (rotating == true)
            { 
            angle += rotateSpeed * Time.deltaTime;
                radius = Random.RandomRange(0.2f, 0.5f);
                var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
                transform.position = centre + offset;

            }
            else
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            if (beam == false)
            {
                StartCoroutine("LightningBeam");
            }

        }
	}

    IEnumerator LightningBeam()
    {
        Debug.Log("beam");
        beam = true;
        yield return new WaitForSeconds(1);
        transform.position = centre;
        rotating = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "End2")
        {
            Destroy(gameObject);
        }
    }
}
