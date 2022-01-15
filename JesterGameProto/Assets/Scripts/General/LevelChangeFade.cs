using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeFade : MonoBehaviour
{

    [SerializeField] GameObject fadeImage;
    [SerializeField] Transform ImageEndPosition;
    [SerializeField] float speed = 5;
    public bool canMove = false, outOfWay = false;


    private void Start()
    {
        
    }


    private void Update()
    {
        if(canMove == true)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, ImageEndPosition.position, speed * Time.deltaTime);

            
        }

        if (outOfWay == true)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, ImageEndPosition.position, speed * Time.deltaTime);

            Destroy(this.gameObject, 1f);
        }
    }

    public void MoveImage()
    {
        canMove = true;
    }
}
