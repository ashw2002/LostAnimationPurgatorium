using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPush : MonoBehaviour
{
    Rigidbody2D rb;
    bool pushing;

    public void Start()
    {
         rb = this.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W)){
            pushing = false;
        }
    }
    private void OnCollisionStay2D(Collision2D col)
    {  
        if (col.gameObject.CompareTag("Player"))
        {
            pushing = true;
            if (pushing){
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    if (col.gameObject.GetComponent<Collider2D>().bounds.min.y > this.gameObject.GetComponent<Collider2D>().bounds.max.y
                    || col.gameObject.GetComponent<Collider2D>().bounds.max.y < this.gameObject.GetComponent<Collider2D>().bounds.min.y)
                    {
                        return;
                    }
                    rb.velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
                }
                else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W))
                {
                    if(col.gameObject.transform.position.x > this.gameObject.GetComponent<Collider2D>().bounds.max.x
                    || col.gameObject.transform.position.x < this.gameObject.GetComponent<Collider2D>().bounds.min.x)
                    {
                        return;
                    }
                    rb.velocity = new Vector3(0f, Input.GetAxis("Vertical"), 0f);
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            ZeroVelocity();
            pushing = false;
        }
    }
    void ZeroVelocity() {
        rb.velocity = new Vector3(0f, 0f, 0f);
    }
}
