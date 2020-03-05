using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    Text text;
    public string instructions;
    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("TutorialText").GetComponent<Text>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            text.text = instructions;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            text.text = null;
            Destroy(this);
        }
    }
}
