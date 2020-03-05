using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wormAttackDetection : MonoBehaviour
{
    Collider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerScript>().parrying == true)
        {
            StartCoroutine(GetComponentInParent<WormWithMouthScript>().GetStunned());
        }
    }
}
