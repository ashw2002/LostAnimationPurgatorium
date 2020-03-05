using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSwitchScript : MonoBehaviour
{
    //When the player interacts with this object it will run this void,
    //then anything that uses this object will detect it's being interacted with and can run their own reset void.
    public bool ResetActivated = false;
    public IEnumerator Reset()
    {
        ResetActivated = true;
        yield return new WaitForSeconds(1);
        ResetActivated = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 0);
        }
    }
}
