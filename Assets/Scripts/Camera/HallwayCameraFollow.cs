using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayCameraFollow : MonoBehaviour
{
    public static HallwayCameraFollow HCF {get; private set;}
    Transform cam;
    GameObject player;
    public bool followingPlayer;
    // Start is called before the first frame update
    void Start(){
        //declares static as this class
        HCF = this;
        //defines cam transform as the main camera's transform position
        cam = Camera.main.gameObject.transform;
        //defines player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate(){
        if (followingPlayer){
            //sets camera position to the player position
            cam.position = new Vector3(player.transform.position.x, cam.position.y, cam.position.z);
        }
    }
}