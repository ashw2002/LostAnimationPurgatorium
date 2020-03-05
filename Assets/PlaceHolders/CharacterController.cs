using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
	Rigidbody2D body;

	float horizontal;
	float vertical;

	public float runSpeed = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
		body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");
    }
    void FixedUpdate()
	{
		body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
	}
}
