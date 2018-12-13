using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControllerScript : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //gameController.AddCoin(1);
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 1);
            Destroy(gameObject);
        }
    }
}
