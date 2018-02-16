using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : Takeble {

    private Rigidbody2D rb;

    protected virtual void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    protected virtual void Update()
    {
        if (IsTaken())
        {
            transform.position = transform.parent.position;
        }
    }
    public override void Take(Transform whoTakes)
    {
        base.Take(whoTakes);
        //rb.simulated = false;
        //rb.bodyType = RigidbodyType2D.Kinematic;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

    }

    public override void Drop()
    {
        base.Drop();
        //rb.simulated = true;
        //rb.bodyType = RigidbodyType2D.Dynamic;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
