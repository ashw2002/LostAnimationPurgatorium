using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPlacementScript : MonoBehaviour
{
    //the perpouse of this script is to hold a gameobject that the player can access
    public GameObject item;
    public float placingSpeed;
    public float progressPercent;
    public Text percent;
    bool itemIsPresent;
    bool primed;


    //start
    private void Start()
    {
        progressPercent = 0;
        itemIsPresent = false;
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space) && itemIsPresent == true && primed == true)
        {
            itemIsPresent = false;
            progressPercent = 0;
            primed = false;
        }

        if(Input.GetKeyUp(KeyCode.Space) && itemIsPresent == true && primed == false)
        {
            primed = true;
        }
        if(progressPercent > 100 && itemIsPresent == false)
        {
            percent.text = ("100%");
            Instantiate(item, transform.position, transform.rotation);
            itemIsPresent = true;
        }
        if(progressPercent <= 100)
        {
            percent.text = (Mathf.RoundToInt(progressPercent) + "%");
        }
    }
}
