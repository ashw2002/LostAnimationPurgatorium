using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float horizontalSpeed;
    public float verticalSpeed;
    bool shieldUp;
    public Collider2D sheildBash;
    public bool damaged;
    public GameObject hitBox;
    public bool parrying;
    public float parryActiveTime;
    SpriteRenderer sprite;
    public bool isPaused;
    GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        parrying = false;
        damaged = false;
        shieldUp = false;
        rb = GetComponent<Rigidbody2D>();
        hitBox.SetActive(false);
        isPaused = false;
        pauseScreen = GameObject.FindGameObjectWithTag("Pause Screen");
        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Input that will trigger a pause screen
        if(Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Backspace))
        {
            if(isPaused == false)
            {
                pauseScreen.SetActive(true);
                isPaused = true;
                rb.velocity = new Vector2(0, 0);
                Time.timeScale = 0;
            }
            else
            {
                pauseScreen.SetActive(false);
                isPaused = false;
                Time.timeScale = 1;
            }
            
        }

        //restricts all other inputs if paused is true
        //temporarily Commented out until ItemInventory is added to the player
        if (isPaused == false/* && ItemInventory.II.InventoryOpen == false*/)
        {
            

            //Shield activation
            if (Input.GetKey(KeyCode.LeftShift))
            {
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
                rb.velocity = new Vector2(horizontalSpeed * Input.GetAxis("Horizontal"), verticalSpeed * Input.GetAxis("Vertical"));
            }
            //Shield bash
            if (Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(Bash());
            }
            //Parry
            if (Input.GetKey(KeyCode.LeftControl))
            {
                StartCoroutine(Parry());
            }
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

        //damages player whether or not they block
        if(collision.gameObject.CompareTag("Unblockable Attack"))
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
        transform.position = new Vector2(GetComponent<PlayerInteractionScript>().checkPointX, GetComponent<PlayerInteractionScript>().checkPointY);
        yield return new WaitForSeconds(.01f);
        damaged = false;
        sprite.material.SetColor("_Color", Color.white);
    }
    //Basic attack
    IEnumerator Bash()
    {
        hitBox.SetActive(true);
        yield return new WaitForSeconds(.01f);
        hitBox.SetActive(false);
    }
    //Parry
    IEnumerator Parry()
    {
        Debug.Log("YEYE");
        parrying = true;
        sprite.material.SetColor("_Color", Color.green);
        yield return new WaitForSeconds(parryActiveTime);
        sprite.material.SetColor("_Color", Color.white);
        parrying = false;
    }

    
}
