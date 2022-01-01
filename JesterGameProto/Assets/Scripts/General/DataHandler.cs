using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    Dataholder dataholder;
    [SerializeField] GameObject levelTwoLockedImage, levelTwoButton;

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
    }

    public void LevelOneDone()
    {
        dataholder.levelOne = true;
        dataholder.SaveData();
    }
}
