using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Baracko : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject hitBox;
    public float chargeStartUpTime;
    public float laserStartUpTime;
    public float spikeCoolDown;
    public float chargeCoolDown;
    public float laserCoolDown;
    SpriteRenderer sprite;
    Color baseColor;
    Collider2D detectionSpace;
    public bool aggroed;
    public GameObject player;
    public float speed;
    bool isAttacking;
    bool isStunned;
    public float attackBuffer;
    public bool hasStarted;
    public int attackMode;
    public int spikeNumber;
    public GameObject spike;
    public float maxHSpikeDist;
    public float maxVSpikeDist;
    public float chargeSpeed;
    public Transform chargeTarget;
    public bool isCharging;
    public int health;
    bool buffering;
    public Text text;
    public float startingPosX;
    public float startingPosY;
    public GameObject key;

    // Start is called before the first frame update
    void Start()
    {
        buffering = false;
        rb = GetComponent<Rigidbody2D>();
        attackMode = 0;
        isAttacking = false;
        aggroed = false;
        detectionSpace = GetComponentInParent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        isStunned = false;
        isCharging = false;
        startingPosX = transform.position.x;
        startingPosY = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.GetComponent<PlayerScript>().isPaused == false)
        {
            //Baracko enters offensive state
            if (aggroed == true && isAttacking == false)
            {
                this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 2);
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed);
                if (hasStarted == false)
                {
                    
                    StartCoroutine(Attack());
                    hasStarted = true;
                }
                if (aggroed == false && isAttacking == false)
                {
                    
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(startingPosX, startingPosY), speed);
                }
            }
            else
            {
                this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 0);
            }
            
            rb.isKinematic = false;
        }
        else
        {
            rb.isKinematic = true;
        }
        if (health <= 0)
        {
            Instantiate(key, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Attacks player
        if (other.CompareTag("Bomb") && buffering == false)
        {
            StartCoroutine(GetHurt());
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
        attackMode = Random.Range(1, 9);
        yield return new WaitForSeconds(attackBuffer);
        isAttacking = true;
        if(attackMode > 0 && attackMode <= 5)
        {
           StartCoroutine (SpikeAttack());
           attackMode = 0;
        }
        if(attackMode >= 6 && attackMode <= 9)
        {
            StartCoroutine(Charge());
            attackMode = 0;
        }
        if(attackMode == 10)
        {
            StartCoroutine(Laser());
            attackMode = 0;
        }
    }

    //Runs spike attack
    IEnumerator SpikeAttack()
    {
        isAttacking = true;
        sprite.material.SetColor("_Color", Color.blue);
        for (int i = 0; i < spikeNumber; i++)
        {
            //creates an instance of a spike
            Instantiate(spike, new Vector2(Random.Range(-maxHSpikeDist, maxHSpikeDist) + player.GetComponent<Transform>().position.x, Random.Range(-maxVSpikeDist, maxVSpikeDist) + player.GetComponent<Transform>().position.y), Quaternion.identity);
        }
        yield return new WaitForSeconds(spikeCoolDown);
        isAttacking = false;
        hasStarted = false;
        sprite.material.SetColor("_Color", Color.white);
    }

    //Runs charge attack
    IEnumerator Charge()
    {
        isAttacking = true;
        yield return new WaitForSeconds(chargeStartUpTime);
        chargeTarget = player.GetComponent<Transform>();
        rb.velocity = new Vector2((chargeTarget.position.x - transform.position.x) * chargeSpeed, (chargeTarget.position.y - (transform.position.y - 8)) * chargeSpeed);
        hitBox.SetActive(true);
        isCharging = true;
        this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 1);
        yield return new WaitForSeconds(2.25f);
        this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 0);
        if (isCharging == true)
        {
            StartCoroutine(StopCharge());
        }
    }

    //runs ending of charge
    IEnumerator StopCharge()
    {
        hitBox.SetActive(false);
        yield return new WaitForSeconds(chargeCoolDown);
        isAttacking = false;
        hasStarted = false;
        
    }
    
    //Runs laser attack (Unused)
    IEnumerator Laser()
    {
        yield return new WaitForSeconds(0);
    }

    IEnumerator GetHurt()
    {
        sprite.material.SetColor("_Color", Color.red);
        health--;
        buffering = true;
        yield return new WaitForSeconds(1);
        buffering = false;
        sprite.material.SetColor("_Color", Color.white);
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Makes him stop charging if hits a wall
        if(isCharging == true)
        {
            rb.velocity = new Vector2(0, 0);
            StartCoroutine(StopCharge());
        }
    }
}
