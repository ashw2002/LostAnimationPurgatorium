using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormWithMouthScript : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject hitBox;
    public float startUpTime;
    public float coolDown;
    SpriteRenderer sprite;
    Color baseColor;
    Collider2D detectionSpace;
    public bool aggroed;
    public GameObject player;
    public float speed;
    bool isAttacking;
    bool isStunned;
    public float stunTime;
    public float startingPosX;
    public float startingPosY;
    public float randomMovementChangeRange;
    float randomMovementChange;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        isAttacking = false;
        aggroed = false;
        detectionSpace = GetComponentInParent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        isStunned = false;
        player = GameObject.FindGameObjectWithTag("Player");
        startingPosX = transform.position.x;
        startingPosY = transform.position.y;
        randomMovementChange = Random.Range(-randomMovementChangeRange, randomMovementChangeRange);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerScript>().isPaused == false)
        {
            //Worm enters offensive state
            if (aggroed == true && isAttacking == false && isStunned == false)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x + randomMovementChange, player.transform.position.y - 1.5f + randomMovementChange), speed);
            }
            if (aggroed == false && isAttacking == false && isStunned == false)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(startingPosX, startingPosY), speed);
            }

        }
    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Attacks player
        if (other.CompareTag("Player") && isAttacking == false && isStunned == false)
        {
            StartCoroutine(Attack());
        }

        //When worm get hit
        if (other.CompareTag("Attack") && isStunned == false)
        { 
            StartCoroutine (GetStunned());
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //Attacks player more
        if (other.CompareTag("Player") && isAttacking == false && isStunned == false)
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
        if (isStunned == false)
        {
            sprite.material.SetColor("_Color", Color.white);
            hitBox.SetActive(true);
            yield return new WaitForSeconds(coolDown);
        }
        hitBox.SetActive(false);
        isAttacking = false;
        randomMovementChange = Random.Range(-randomMovementChangeRange, randomMovementChangeRange);
    }

    //Stops worm from acting when stunned
    public IEnumerator GetStunned()
    {
        isStunned = true;
        sprite.material.SetColor("_Color", Color.blue);
        yield return new WaitForSeconds(stunTime);
        sprite.material.SetColor("_Color", Color.white);
        isStunned = false;
    }
}

