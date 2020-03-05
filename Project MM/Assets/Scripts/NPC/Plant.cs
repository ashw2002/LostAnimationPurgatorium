using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public GameObject hitBox;
    public float startUpTime;
    public float coolDown;
    SpriteRenderer sprite;
    Color baseColor;
    public bool aggroed;
    public GameObject player;
    bool isAttacking;
    public GameObject PoisonRag;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        aggroed = true;
        sprite = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        //Eats corpse and becomes satisfied
        if (other.CompareTag("Corpse"))
        {
            sprite.material.SetColor("_Color", Color.white);
            Instantiate(PoisonRag, other.gameObject.transform.position, transform.rotation);
            Destroy(other.gameObject);
            aggroed = false;
        }
        //Attacks player
        if (other.CompareTag("Player") && !isAttacking && aggroed == true)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Eats corpse and becomes satisfied
        if (other.CompareTag("Corpse"))
        {
            Debug.Log("Is satisfied");
            sprite.material.SetColor("_Color", Color.white);
            Instantiate(PoisonRag, other.gameObject.transform.position, transform.rotation);
            Destroy(other.gameObject);
            aggroed = false;
        }
        //Attacks player more
        if (other.CompareTag("Player") && !isAttacking && aggroed == true)
        {
            StartCoroutine(Attack());
        }
    }

    //What happens when attack
    IEnumerator Attack()
    {
        isAttacking = true;
        sprite.material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(startUpTime);
        if (aggroed == true)
        {
            sprite.material.SetColor("_Color", Color.white);
            hitBox.SetActive(true);
            this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 1);
            yield return new WaitForSeconds(coolDown);
            this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 0);
        }
        hitBox.SetActive(false);
        isAttacking = false;
    }
}
