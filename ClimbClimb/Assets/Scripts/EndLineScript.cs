using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLineScript : MonoBehaviour {
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
