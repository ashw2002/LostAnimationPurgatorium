using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItemScript : MonoBehaviour
{
    public bool consumable;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                collision.gameObject.GetComponent<PlayerScript>().damaged = false;
                if(consumable == true)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
