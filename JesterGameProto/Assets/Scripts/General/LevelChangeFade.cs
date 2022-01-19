using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeFade : MonoBehaviour
{

    [SerializeField] GameObject fadeImage;
    [SerializeField] Transform ImageSpawnPosition, targetFolder;
    //[SerializeField] float speed = 5;
    public bool canMove = false, outOfWay = false;


    private void Start()
    {
        
    }


    private void Update()
    {
        if(canMove == true)
        {
            canMove = false;
            //transform.position = Vector3.MoveTowards(this.transform.position, ImageEndPosition.position, speed * Time.deltaTime);

            GameObject movingImage = Instantiate(fadeImage, ImageSpawnPosition.position, Quaternion.identity, targetFolder.transform);

            
            
        }

        else if (outOfWay == true)
        {
            outOfWay = false;
            //transform.position = Vector3.MoveTowards(this.transform.position, ImageEndPosition.position, speed * Time.deltaTime);

            //Destroy(this.gameObject, 1f);

            GameObject movingImage = Instantiate(fadeImage, ImageSpawnPosition.position, Quaternion.identity, targetFolder.transform);

            Destroy(movingImage, 2f);
        }
    }

    public void MoveImage()
    {
        canMove = true;
    }
}
