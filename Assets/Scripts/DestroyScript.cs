using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    public GameObject canvas;

    void Start()
    {
        canvas.SetActive(false);
        Destroy(gameObject, 10);
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(9);
        canvas.SetActive(true);
    }

}
