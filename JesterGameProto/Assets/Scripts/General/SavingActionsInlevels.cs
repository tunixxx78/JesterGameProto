using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingActionsInlevels : MonoBehaviour
{
    Dataholder dataholder;
    SFXManager sFXManager;


    private void Awake()
    {
        dataholder = FindObjectOfType<Dataholder>();
        sFXManager = FindObjectOfType<SFXManager>();
    }

    public void LevelOneDone()
    {
        sFXManager.button.Play();
        dataholder.levelOne = true;
        dataholder.SaveData();
    }
    public void LevelTwoDone()
    {
        sFXManager.button.Play();
        dataholder.levelTwo = true;
        dataholder.SaveData();
    }
    public void LevelThreeDone()
    {
        sFXManager.button.Play();
        dataholder.levelThree = true;
        dataholder.SaveData();
    }
    public void LevelFourDone()
    {
        sFXManager.button.Play();
        dataholder.levelFour = true;
        dataholder.SaveData();
    }
}
