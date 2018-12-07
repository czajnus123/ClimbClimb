using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetScript : MonoBehaviour {

    bool done;
    int i;
    string bgname;

	// Use this for initialization
	void Start () {
        i = 0;
        fitWidth();
        // fitHeight();
    }
	
	// Update is called once per frame
	void Update () {
        fitWidth();
    }

    void fitWidth()
    {
        if (i < 1)
            bgname = "Background";
        else if (i >= 1 && i < 2)
            bgname = "ShopBackground";
        else if (i >= 2)
            i = 0;
        SpriteRenderer mapRenderer;
        float worldScreenHeight;
        float worldScreenWidth;

        float unitWidth;
        float unitHeight;
        try
        {
            mapRenderer = GameObject.Find(bgname).GetComponent<SpriteRenderer>();
            mapRenderer.sprite.texture.filterMode = FilterMode.Point;
            Sprite s = mapRenderer.sprite;
            unitWidth = s.textureRect.width / s.pixelsPerUnit;
            unitHeight = s.textureRect.height / s.pixelsPerUnit;

            worldScreenHeight = GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize * 2.0f;
            worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
            mapRenderer.transform.localScale = new Vector3(worldScreenWidth / unitWidth / gameObject.transform.localScale.x, worldScreenHeight / unitHeight / gameObject.transform.localScale.y);
            i++;
        }
        catch
        {

        }



    }

    void fitHeight()
    {
        SpriteRenderer mapRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        // Set filterMode
        mapRenderer.sprite.texture.filterMode = FilterMode.Point;

        // Get stuff
        double width = mapRenderer.sprite.bounds.size.y;
        double worldScreenHeight = GameObject.Find("Main Camera").GetComponent<Camera>().orthographicSize * 2.0f;

        // Resize
        transform.localScale = new Vector2(1, 1) * (float)(worldScreenHeight / width);
    }
}
