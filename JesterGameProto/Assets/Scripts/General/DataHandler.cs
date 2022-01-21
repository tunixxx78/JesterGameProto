using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{

    Dataholder dataholder;
    [SerializeField] GameObject levelTwoLockedImage, levelTwoButton, levelThreeLockedImage, levelThreeButton, levelFourLockedImage, levelFourButton,
                                levelFiveLockedImage, levelFifeButton;

    SFXManager sFXManager;

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
        if(dataholder.levelThree == true)
        {
            levelFourLockedImage.SetActive(false);
            levelFourButton.SetActive(true);
        }
        if(dataholder.levelFour == true)
        {
            levelFiveLockedImage.SetActive(false);
            levelFifeButton.SetActive(true);
        }
        
    }

    
}
