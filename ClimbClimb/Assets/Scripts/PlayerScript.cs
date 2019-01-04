using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private GameObject posRight, posLeft;

    private SaveScript saveManager;
    private Touch touch;
    public Sprite[] skins;
    public Material[] trails;

    public bool rightSide,check;
    private bool left, leftSlam, rightSlam, tapped;



	// Use this for initialization
	void Start () {

        rightSide = GameControllerScript.Instance.rightSide;
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveScript>();
        left = false;
        tapped = false;
        leftSlam = false;
        rightSlam = false;

        posRight = GameObject.Find("posRight");
        posLeft = GameObject.Find("posLeft");
    }
	
	// Update is called once per frame
	void Update () {

        if (GameControllerScript.Instance.gameOver== false && gameObject.GetComponent<SpriteRenderer>().enabled==true)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
            }
                if (touch.phase == TouchPhase.Began)
                {
                    tapped = true;
                }
                if (tapped == true)
                {
                    if (rightSide == true)
                    {
                    gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, posLeft.transform.position, 2);
                    GameObject.Find("RightParticle").GetComponent<ParticleSystem>().Stop();
                    

                     if (gameObject.transform.position.x == posLeft.transform.position.x)
                     {
                        GameObject.Find("LeftParticle").GetComponent<ParticleSystem>().Play();
                    }

                    GameControllerScript.Instance.rightSide = false;
                    }
                    else if (rightSide == false)
                    {
                    gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, posRight.transform.position, 2);
                    
                    GameObject.Find("LeftParticle").GetComponent<ParticleSystem>().Stop();


                    if (gameObject.transform.position.x == posRight.transform.position.x)
                    {
                        GameObject.Find("RightParticle").GetComponent<ParticleSystem>().Play();
                    }

                    GameControllerScript.Instance.rightSide = true;
                    }
                    if (gameObject.transform.position == posLeft.transform.position)
                    {
                        tapped = false;
                        rightSide = false;
                    }
                    else if (gameObject.transform.position == posRight.transform.position)
                    {
                        tapped = false;
                        rightSide = true;
                    }
                }
            }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {

        gameObject.GetComponent<SpriteRenderer>().enabled = false;


        StartCoroutine(Death(collision));

        if (collision.gameObject.tag != "Coin"&&collision.gameObject.tag!="ObstacleClone")
        {
            GameControllerScript.Instance.SetDeathCount();
        }


       
            
    }
    IEnumerator Death(Collision2D collision)
    {
        GameControllerScript.Instance.gameOver = true;
        GameControllerScript.Instance.endMenu = true;
        FindObjectOfType<AudioManagerScript>().Pause("Theme");
        try
        {
            GameObject.Find("LeftParticle").GetComponent<ParticleSystem>().Stop();
            GameObject.Find("RightParticle").GetComponent<ParticleSystem>().Stop();
            GameObject.Find("ExplodeParticle").GetComponent<ParticleSystem>().Play();
            GameObject.Find("ExplodeParticle2").GetComponent<ParticleSystem>().Play();

            Destroy(collision.transform.parent.gameObject);

        }
        catch
        {
            Destroy(collision.gameObject);
        }
        yield return new WaitForSeconds(1f);

        GameObject.Find("Canvas").transform.Find("Panel").gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.Find("Texts").gameObject.SetActive(false);
        saveManager.Save();
        if (GameControllerScript.Instance.deathCount > 1)
        {
            FindObjectOfType<AudioManagerScript>().Stop("Theme");
            Destroy(gameObject);
        }


    }

    public bool GetRightSidePlayer()
    {
        return rightSide;
    }
}
