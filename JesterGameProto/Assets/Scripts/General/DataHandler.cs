using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    Dataholder dataholder;
    [SerializeField] GameObject levelTwoLockedImage, levelTwoButton, levelThreeLockedImage, levelThreeButton;

    private void Awake()
    {
        dataholder = FindObjectOfType<Dataholder>();
    }

    private void Update()
    {
        if(dataholder.levelOne == true)
        {
            levelTwoLockedImage.SetActive(false);
            levelTwoButton.SetActive(true);
        }
        if(dataholder.levelTwo == true)
        {
            levelThreeLockedImage.SetActive(false);
            levelThreeButton.SetActive(true);
        }
    }

    public void LevelOneDone()
    {
        dataholder.levelOne = true;
        dataholder.SaveData();
    }
    public void LevelTwoDone()
    {
        dataholder.levelTwo = true;
        dataholder.SaveData();
    }
}
