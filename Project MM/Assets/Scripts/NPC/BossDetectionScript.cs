using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDetectionScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponentInChildren<Baracko>().aggroed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponentInChildren<Baracko>().aggroed = false;
        }
    }
}
