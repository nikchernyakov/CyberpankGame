using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHandler : MonoBehaviour {

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !CheckJump())
        {
            Debug.Log("lol");
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        //Debug.Log(rb.velocity);
	}

    public bool CheckJump()
    {
        return Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W);
    }
    
    public void Jump(float jumpVelocity)
    {
        rb.velocity = Vector2.up * jumpVelocity;
    }
}
