using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSwitch : MonoBehaviour
{
    public GameObject outPut;
    public int activationCharges;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyUp(KeyCode.Space) && activationCharges > 0)
        {
            StartCoroutine(Trick());
        }
    }

    IEnumerator Trick()
    {
        yield return new WaitForSeconds(.01f);
        Instantiate(outPut, transform.position, transform.rotation);
        activationCharges--;
    }
}
