﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInteractionScript : MonoBehaviour
{
    public static PlayerInteractionScript PIS { get; private set; }

    public Collider2D collider;
    public Image doorUI;
    public float checkPointX;
    public float checkPointY;
    public Camera cam;
    public float cameraSpeed;
    public float cameraIncreaseRate;
    Rigidbody2D rb;

    private GameObject player;
    
    //Triggered checks for if an item has been collected in the current frame.
    //Prevents an object from being picked up twice.
    private bool triggered;

    public Text coinReadout;
    // Start is called before the first frame update
    void Start()
    {
        PIS = this;
        rb = GetComponent<Rigidbody2D>();
        doorUI.enabled = false;
        checkPointX = transform.position.x;
        checkPointY = transform.position.y;
        player = GameObject.FindGameObjectWithTag("Player");
        

    }
    //Refreshes the triggered boolean every frame
    private void Update()
    {
        ItemInventory.II.CollectCheck();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Determines when the UI indicator for the door disappears
        if (collision.gameObject.CompareTag("Door")/*|| collision.gameObject.CompareTag("BossDoor")*/)
        {
            doorUI.enabled = true;
        }
        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Determines when UI indicator for doors disappears
        if (collision.gameObject.CompareTag("Door")/*|| collision.gameObject.CompareTag("BossDoor")*/)
        {
            doorUI.enabled = false;
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Allows the player to place items
        if(collision.gameObject.CompareTag("Item Placement"))
        {
            if (Input.GetKey(KeyCode.Space) && GameObject.FindGameObjectWithTag("Bomb") == null)
            {
                StartCoroutine(PlaceItem());
            }
        }

        //Changes the position of the camera when the player enters a new position
        if (collision.gameObject.CompareTag("Camera Mover"))
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(collision.transform.position.x, collision.transform.position.y, cam.transform.position.z), cameraSpeed);
            cam.orthographicSize += ((collision.gameObject.GetComponent<CameraPositionSizeHolder>().size - cam.orthographicSize) * cameraIncreaseRate * Time.deltaTime);
        }

        if (collision.gameObject.CompareTag("Door"))
        {
            //Transports through doors when you hit R
            if (Input.GetKeyUp(KeyCode.Space) && GameObject.FindGameObjectWithTag("Bomb") == null)
            {
                base.transform.position = collision.gameObject.GetComponent<DoorScript>().otherDoor.transform.position;
                
            }
        }
        /*if(collision.gameObject.CompareTag("Boss Door"))
        {
            //Transports through doors when you hit R
            if (Input.GetKeyUp(KeyCode.R))
            {
                SceneManager.LoadScene(collision.gameObject.GetComponent<BossDoorScript>().newScene, LoadSceneMode.Single);
            }
        }*/

        IEnumerator PlaceItem()
        {
            yield return new WaitForSeconds(.01f);
            collision.gameObject.GetComponent<ItemPlacementScript>().progressPercent += collision.gameObject.GetComponent<ItemPlacementScript>().placingSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider col)
    {
        if (col.CompareTag("Collectable")){
            ItemInventory.II.canCollect = true;
            ItemInventory.II.currentCollectable = col.gameObject;
        }
    }
}
