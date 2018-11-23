using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControllerScript : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //gameController.AddCoin(1);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) + 1);
            Destroy(gameObject);
        }
    }
}
