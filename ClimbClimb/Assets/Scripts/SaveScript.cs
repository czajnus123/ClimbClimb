using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveScript : MonoBehaviour {

    private PointManagerScript pointManager;

    private float skin;
    private float hiScore;

	// Use this for initialization
	void Start () {

        pointManager = GameObject.Find("PointManager").GetComponent<PointManagerScript>();
        Load();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerDataClass data = new PlayerDataClass();

        data.hiScore = pointManager.GetHiScore();    //zapis do pliku. Podmienia hiscore w pliku na hiscore aktualny w grze

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat",FileMode.Open);
            PlayerDataClass data = (PlayerDataClass)bf.Deserialize(file);
            file.Close();

            hiScore = data.hiScore;            //pobiera hiscore z pliku i przypisuje do lokalnej zmiennej
            pointManager.SetHiScore(hiScore);  //ustawia globalna zmienna hiscore na hiscore z lokalnej zmiennej
           // skin = data.skin;
        }
    }
}

[Serializable]
class PlayerDataClass
{
    public float skin;
    public float hiScore;

}
