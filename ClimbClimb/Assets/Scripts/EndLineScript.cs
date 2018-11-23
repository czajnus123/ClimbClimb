using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLineScript : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Lightning")
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
}
