using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public float activateTime;
    public float deactivateTime;
    Collider2D hitBox;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        hitBox = GetComponent<Collider2D>();
        hitBox.enabled = false;

        //starts the spike instantly as it instantiates
        StartCoroutine(Activate());
    }

    IEnumerator Activate()
    {
        yield return new WaitForSeconds(activateTime);
        hitBox.enabled = true;
        sprite.material.SetColor("_Color", Color.red);
        this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 1);
        yield return new WaitForSeconds(deactivateTime);
        Destroy(gameObject);
    }
}
