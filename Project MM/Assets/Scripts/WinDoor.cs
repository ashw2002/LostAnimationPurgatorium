using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinDoor : MonoBehaviour
{
    public Text winText;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (string s in ItemInventory.II.items)
                {
                    if (Input.GetKey(KeyCode.Space) && (s == "Key(Clone)"))
                    {
                    winText.enabled = true;
                    winText.text = "You Win!";
                    }
                }
        }
    }
}
