using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject hitBox;
    bool primed;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && primed == true)
        {
            StartCoroutine(Boom());
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            primed = true;
        }
    }

    IEnumerator Boom()
    {
        hitBox.SetActive(true);
        yield return new WaitForSeconds(.000001f);
        Destroy(gameObject);
    }
}
