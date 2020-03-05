using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSwitchDetectorScript : MonoBehaviour
{
    public float startingPosX;
    public float startingPosY;
    public float startingRotZ;
    public GameObject resetButton;
    
    // Start is called before the first frame update
    void Start()
    {
        //Sets it's starting position for if it needs to be rest later
        startingPosX = transform.position.x;
        startingPosY = transform.position.y;
        startingRotZ = transform.eulerAngles.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (resetButton.GetComponent<ResetSwitchScript>().ResetActivated == true)
        {
            Reset();
        }
    }

    //Runs when reset button is pressed and will take theis to it's starting position and rotation
    private void Reset()
    {
        transform.position = new Vector2(startingPosX, startingPosY);
        transform.eulerAngles = new Vector3(0, 0, startingRotZ);
    }
}
