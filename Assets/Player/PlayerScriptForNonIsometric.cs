
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*

public class PlayerScriptForNonIsometric : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Collider2D collider;
    Animator animator;

    //bool that helps with turning the sprites around based on player input
    bool facingLeft = true;
    //Controls how fast the characters run on the ground
    public float maxSpeed;

    //helps to determine whether the characters are grounded or not
    bool grounded = false;
    //determines how high the characters can jump
    public float jumpPower;
    //These three determine how holding a jump button keeps the player in the air
    public float jumpTime;
    public float jumpTimeCounter;
    public bool stoppedJumping;

    //Determines how fast the character falls when fast fall button is pushed
    public float downSpeed;
    //Determines if the player has fast fallen
    public bool hasFastFallen;
    //gets sound effect for the characters jump
    public AudioClip jumpAudio;
    public float loadTime;

    //START
    void Start()
    {
        //gets animator fot the character
        animator = GetComponent<Animator>();
        //Gets collider for character
        collider = GetComponent<Collider2D>();
        //Starts out the characters with not having fast fallen
        hasFastFallen = false;
        //Helps make holding down the jump button keep the character in the air for a bit longer
        jumpTimeCounter = jumpTime;
        //Makes blueRB reference the blue characeters Rigid body
        rb = GetComponent<Rigidbody2D>();
        //Makes blueRenderer reference the blue characters renderer
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    //END

    //UPDATE
    void FixedUpdate()
    {
        //Sets falling animation to blue character when he's falling
        if (rb.velocity.y < -0.1)
        {
            animator.SetInteger("AnimState", 3);
        }

        //Checks the "Horizontal2 axis 
        float move = Input.GetAxis("Horizontal");
        //Makes the character move depending on the Horizontal2 axis value
        rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);
        //Determines which way the character is facing and if their current movement should change which direction their facing, then changes the direction accordingly
        if (move < 0 && !facingLeft)
        {
            Flip();
        }
        else if (move > 0 && facingLeft)
        {
            Flip();
        }
        //Gives arunning animation to the blue character if their Horizontal2 axis doesn't equal 0, and if their on the ground
        if (move == 0 && grounded)
        {
            animator.SetInteger("AnimState", 0);
        }
        if (move != 0 && grounded)
        {
            animator.SetInteger("AnimState", 1);
        }
        //checks if the character is grounded
        if (grounded)
        {
            //sets the timer that ghelps make the character stay in the air if the jump button is being held
            jumpTimeCounter = jumpTime;
        }
        //Checks whether the character is grounded  and if the Jump axis value is greater than 0
       /* if (grounded && Input.GetKey(KeyCode.Space))
        {
            //makes the blue character jump
            rb.velocity = Vector2.up * jumpPower;
            rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            //Tells the rest of the code that the character has jumped
            stoppedJumping = false;
            //plays "jump" sound effect
            AudioSource.PlayClipAtPoint(jumpAudio, transform.position);
            //Plays the jumping animation
            animator.SetInteger("AnimState", 2);
        }
        //This checks if the Jump axis value is greater than 0 and if the character has actually jumped
        if ((Input.GetKey(KeyCode.Space) && !stoppedJumping))
        {
            //checks the jumpTimeCounter
            if (jumpTimeCounter > 0)
            {
                //makesthe character go a bit higher if the jump button ism still being pushed
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpTimeCounter -= Time.deltaTime;
            }
        }
        //Checks if the jump button is still being pushed or not
        if (Input.GetKey(KeyCode.Space))
        {
            //Makes the character stop going up if the jump button isn't being pressed anymore
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        //Constsantly makes the down variable equal to the Vertical axis value
        float down = Input.GetAxis("Vertical");
        //determines whether the down variable is less than 0
        if (down < 0)
        {
            //determines if the character has already fast fallen
            if (hasFastFallen == false)
            {
                //makes the player fall faster
                rb.velocity = new Vector2(rb.velocity.x, 0);
                //tells the rest of the code that the character has fast fallen
                hasFastFallen = true;
            }
            //Makes the blue character go down faster
            rb.AddForce(new Vector2(0, downSpeed * down), ForceMode2D.Impulse);
        }
    }//END UPDATE

    //This determines what happens when void is called and flips around the character if needed
    void Flip()
    {
        facingLeft = !facingLeft;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    //These voids determine whether or not the character is grounded or if they are touching the grappling hook
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    //checks if the character leaves the ground, if so they can fast fall
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            hasFastFallen = false;

        }
    }

}
//Written by Jonah Pearson

    */
    