using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeFade : MonoBehaviour
{

    [SerializeField] GameObject fadeImage; //Image what gets instantiated and moved(this is prefab, with animation builded in)
    [SerializeField] Transform ImageSpawnPosition, targetFolder; //Spawnposition for fadeImage and target folder for instatiated fadeimage
    //[SerializeField] float speed = 5;
    public bool canMove = false, outOfWay = false; //Boolian variables for main functionalitys - moving image.


    private void Start()
    {
        
    }


    private void Update()
    {
        //This happens when current match has ended and canMove variable has turned to True.
        
        if(canMove == true)
        {
            //canMove variable turn to False, so following instantiate will happens only once.
            canMove = false;
           
           //This instantiate fadeImage to scene, animation kicks in and moves that to wanted position.
            GameObject movingImage = Instantiate(fadeImage, ImageSpawnPosition.position, Quaternion.identity, targetFolder.transform);

            
            
        }
        //This happens every time you enter into new scene -> it removes scenechange-panel out of the way. 
        else if (outOfWay == true)
        {
            // outOfWay variable turns to False so following instantiate will happens only once
            outOfWay = false;
            // This instantiate fadeImage to scene, animation kicks in and moves that to wanted position. When wanted position is reached, image will be destroyed.
            GameObject movingImage = Instantiate(fadeImage, ImageSpawnPosition.position, Quaternion.identity, targetFolder.transform);
            //Destroying image
            Destroy(movingImage, 2f);
        }
    }

//This is called from game manager script when current battle has ended and its time to change scene
    public void MoveImage()
    {
    // canMove variable is turned True and endFade is possible
        canMove = true;
    }
}
