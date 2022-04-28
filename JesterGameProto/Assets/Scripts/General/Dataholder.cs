using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;


// saving and loading game data functionality

public class Dataholder : MonoBehaviour
{
    public static Dataholder dataInstance;

    public bool levelOne, levelTwo, levelThree, levelFour, levelFive; //variables for data that will be saved at some point in game

    private void Awake()
    {
        LoadData();

        if (dataInstance != null && dataInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        dataInstance = this;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
    //Only development use! Resets all saving sata.
        if (Input.GetKeyDown(KeyCode.R))
        {
            levelOne = false;
            levelTwo = false;
            levelThree = false;
            levelFour = false;
            levelFive = false;

            SaveData();
        }
    }

    public void SaveData()
    {
    //Creates saving file and saves data in binary format
    
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveGame.dat");
        LevelData data = new LevelData();

        data.levelOne = levelOne;
        data.levelTwo = levelTwo;
        data.levelThree = levelThree;
        data.levelFour = levelFour;
        data.levelFive = levelFive;

        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadData()
    {
    // checks if there is saved data
        if(File.Exists(Application.persistentDataPath + "/SaveGame.dat"))
        {
        // if data can be found, this get it out from saved file
        
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveGame.dat", FileMode.Open);
            LevelData data = (LevelData)bf.Deserialize(file);

            levelOne = data.levelOne;
            levelTwo = data.levelTwo;
            levelThree = data.levelThree;
            levelFour = data.levelFour;
            levelFive = data.levelFive;

        }
    }

}

[Serializable]

class LevelData
{
    public bool levelOne, levelTwo, levelThree, levelFour, levelFive;
}
