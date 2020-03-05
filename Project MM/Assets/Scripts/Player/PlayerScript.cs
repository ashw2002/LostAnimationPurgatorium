using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float runSpeed;
    public float walkSpeed;
    bool shieldUp;
    public Collider2D sheildBash;
    public bool damaged;
    public GameObject hitBox;
    public bool parrying;
    public float parryActiveTime;
    SpriteRenderer sprite;
    public bool isPaused;
    GameObject pauseScreen;
    public bool isCarrying;
    private bool triggered;
    GameObject corpse;
    GameObject deathScreen;
    GameObject tutorialText;
    public Sprite sheild;


    // Start is called before the first frame update
    void Start()
    {
        tutorialText = GameObject.Find("TutorialText");
        sprite = GetComponent<SpriteRenderer>();
        parrying = false;
        damaged = false;
        shieldUp = false;
        rb = GetComponent<Rigidbody2D>();
        hitBox.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        pauseScreen = GameObject.FindGameObjectWithTag("Pause Screen");
        pauseScreen.SetActive(false);
        isCarrying = false;
        deathScreen = GameObject.Find("DeathScreen");
        deathScreen.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        triggered = false;

        

        //lets the corpse act as it's own entity
        if(isCarrying == true && corpse != null)
        {
            corpse.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if(corpse != null)
        {
            corpse.GetComponent<SpriteRenderer>().enabled = true;
        }

        //Input that will trigger a pause screen
        if(Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Backspace))
        {
            if (isPaused == false)
            {
                tutorialText.SetActive(false);
                pauseScreen.SetActive(true);
                isPaused = true;
                rb.velocity = new Vector2(0, 0);
                Time.timeScale = 0;
            }
            else
            {
                tutorialText.SetActive(true);
                pauseScreen.SetActive(false);
                isPaused = false;
                Time.timeScale = 1;
            }
        }

        if(ItemInventory.II.InventoryOpen == true){
            rb.velocity = new Vector2(0, 0);
            this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 0);
        }

        if(isCarrying == true && corpse != null)
        {
            corpse.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y - 1.5f);
        }

        //restricts all other inputs if paused is true
        //temporarily Commented out until ItemInventory is added to the player
        if (isPaused == false && ItemInventory.II.InventoryOpen == false && sprite.enabled == true)
        {

            /*
             * This is old code commented out to show our progression in thought
             * if (rb.velocity.magnitude != 0 && Input.GetKey(KeyCode.LeftShift))
            {
                this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 2);
            }
            else if (rb.velocity.magnitude != 0)
            {
                this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 1);
            }
            else
            {
                this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 0);
            }*/
            if (rb.velocity.magnitude != 0)
            {
                //these are all the possible player movement animations
                if (isCarrying)
                {
                    this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 4);
                }else if (Input.GetKey(KeyCode.LeftShift)){
                    this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 2);
                }
                else
                {
                    this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 1);
                }
            }
            else
            {
                //these are all the possible idle animations for the player
                if (isCarrying)
                {
                    this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 3);
                }
                else
                {
                    this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 0);
                }
                
            }
             //Shield activation
            if (Input.GetKey(KeyCode.K))
            {
                this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 8);
                shieldUp = true;
            }
            else
            {
                shieldUp = false;
            }

            //Movement
            if (shieldUp == true)
            {
                rb.velocity = new Vector2(0, 0);
            }

            else
            {
                if (Input.GetKey(KeyCode.LeftShift) && isCarrying == false)
                {
                    rb.velocity = new Vector2(runSpeed * Input.GetAxisRaw("Horizontal"), runSpeed * Input.GetAxisRaw("Vertical"));
                    //Flips Sprite in the correct direction, based on movement
                    if (Input.GetAxisRaw("Horizontal") < 0)
                    {
                        this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                      
                    }
                    else if (Input.GetAxisRaw("Horizontal") > 0)
                    {
                        this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                        
                    }
                }
                else { 
                    rb.velocity = new Vector2(walkSpeed * Input.GetAxisRaw("Horizontal"), walkSpeed * Input.GetAxisRaw("Vertical"));
                    //Flips Sprite in the correct direction, based on movement
                    if (Input.GetAxisRaw("Horizontal") < 0)
                    {
                        this.gameObject.GetComponent<SpriteRenderer>().flipX = true;

                    }
                    else if (Input.GetAxisRaw("Horizontal") > 0)
                    {
                        this.gameObject.GetComponent<SpriteRenderer>().flipX = false;


                    }
                    
                }
               
                
      
            }
            
            if (Input.GetKey(KeyCode.J) && isCarrying == false)
            {
                StartCoroutine(Bash());
            }
            //Parry
            if (Input.GetKey(KeyCode.L ) && parrying == false)
            {
                
                StartCoroutine(Parry());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Corpse"))
        {
            isCarrying = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Damages/kills the player when they enter an attack
        if (collision.gameObject.CompareTag("Attack") && shieldUp == false && parrying == false)
        {
            if(damaged == true)
            {
                StartCoroutine (Die());
            } else if(damaged == false)
            {
                StartCoroutine(Damage());
                sprite.material.SetColor("_Color", Color.red);
            }
        }

        //Sets object as new object you're set to
        if (collision.gameObject.CompareTag("Corpse"))
        {
            corpse = collision.gameObject;
        }

        //damages player whether or not they block
        if(collision.gameObject.CompareTag("Unblockable Attack") && parrying == false)
        {
            if (damaged == true)
            {
                StartCoroutine(Die());
            }
            else if (damaged == false)
            {
                StartCoroutine(Damage());
                sprite.material.SetColor("_Color", Color.red);
            }
        }

        //Kills player if they get hit with a bomb level attack
        if (collision.gameObject.CompareTag("Bomb"))
        {
            StartCoroutine(Die());
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        //Lets you pick up corpse
        if (collision.gameObject.CompareTag("Corpse") )
        {
            if(isCarrying == true)
            {
                corpse.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y - 1.5f);
            }

            if (((Input.GetKeyUp(KeyCode.Space) && isCarrying == false) || (Input.GetKeyUp(KeyCode.J) && isCarrying == true)) && !triggered)
            {
                if (collision.gameObject.CompareTag("Corpse"))
                {
                    StartCoroutine(ToggleCarrying());
                }
                triggered = true;
            }
        }

    }
    //buffer as to not die instantly
    IEnumerator Damage()
    {
        yield return new WaitForSeconds(.01f);
        damaged = true;
    }
    //Player dies
    IEnumerator Die()
    {
        this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 6);
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 0;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        //sprite.enabled = false;
        deathScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);
        deathScreen.SetActive(false);
        transform.position = new Vector2(GetComponent<PlayerInteractionScript>().checkPointX, GetComponent<PlayerInteractionScript>().checkPointY);
        damaged = false;
        sprite.material.SetColor("_Color", Color.white);
        isCarrying = false;
        sprite.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        Time.timeScale = 1;
        this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 7);
    }
    //Basic attack
    IEnumerator Bash()
    {
        hitBox.SetActive(true);
        this.gameObject.GetComponent<Animator>().SetInteger("AnimState", 5);
        yield return new WaitForSeconds(.01f);
        hitBox.SetActive(false);
    }
    //Parry
    IEnumerator Parry()
    {
        parrying = true;
        sprite.material.SetColor("_Color", Color.green);
        
        yield return new WaitForSeconds(parryActiveTime);
        sprite.material.SetColor("_Color", Color.white);
        parrying = false;
    }
    //Lets isCarrying be toggled without bugging out
    IEnumerator ToggleCarrying()
    {
        isCarrying = !isCarrying;
        yield return new WaitForEndOfFrame();
    }
    //Adds a buffer to moving around the corpse
    IEnumerator BufferCorpseMove()
    {
        yield return new WaitForSeconds(.000001f);
        corpse.transform.position = transform.position;
    }
}
