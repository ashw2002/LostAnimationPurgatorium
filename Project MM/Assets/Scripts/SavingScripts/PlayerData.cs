using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData {
    //Holds the information to be saved

    public List<string> inventory;
	public float[] position;
    public int coins;


    //this is the script that gets saved and loaded

    //Sets variables to be saved
    public PlayerData (ItemInventory player)
	{
        inventory = player.items;

        coins = player.coins;

		position = new float[3];

		position[0] = player.transform.position.x;
		position[1] = player.transform.position.y;
		position[2] = player.transform.position.z;
	}
}

