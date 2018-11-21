using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLineScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {

        try
        {
            Destroy(collision.transform.parent.gameObject);
        }
        catch
        {
            Destroy(collision.gameObject);
        }

    }
}
