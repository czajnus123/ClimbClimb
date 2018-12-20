﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour {

    private GameObject player, posRight, posLeft;

    private float speed = 0;
    private bool wyprzedzoned = false;
    int changeSideCounter = 0;

    public void SetSpeed(float value)
    {
        this.speed = value;
    }

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        posRight = GameObject.Find("posRight");
        posLeft = GameObject.Find("posLeft");
        GetComponent<SpriteRenderer>().sprite = GameControllerScript.Instance.skins[Random.Range(0, GameControllerScript.Instance.skins.Length - 1)];
    }

    private void Update()
    {
        if (GameControllerScript.Instance.gameOver==false)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (gameObject.transform.position.y < player.transform.position.y && !wyprzedzoned)
            {
             wyprzedzoned = true;
             PlayerPrefs.SetInt("position", PlayerPrefs.GetInt("position", 1000) - 1);
            }
        }
        else if(GameControllerScript.Instance.gameOver && speed>0)
        {
            Destroy(gameObject);
        }
        
    }

    public void change_side()
    {
        if (this.transform.position.x > 0)
        {
            GameObject.Find("ORightParticle").GetComponent<ParticleSystem>().Stop();
            GameObject.Find("OLeftParticle").GetComponent<ParticleSystem>().Play();
            while (this.transform.position.x != posLeft.transform.position.x)
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position,
                    new Vector2(posLeft.transform.position.x, gameObject.transform.position.y), 2);
        }
        else
        {
            GameObject.Find("ORightParticle").GetComponent<ParticleSystem>().Play();
            GameObject.Find("OLeftParticle").GetComponent<ParticleSystem>().Stop();
            while (this.transform.position.x != posRight.transform.position.x)
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position,
                    new Vector2(posRight.transform.position.x, gameObject.transform.position.y), 2);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision: " + collision.gameObject.tag);
        if (collision.gameObject.tag != "Player")
        {

            change_side();

        }
            
        if (collision.gameObject.name == "End")
            Destroy(gameObject);
    }
}
