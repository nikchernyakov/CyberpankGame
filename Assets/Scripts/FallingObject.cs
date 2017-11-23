using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : Takeble {

    private Rigidbody2D rb;

    protected virtual void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    public override void Take(Transform whoTakes)
    {
        base.Take(whoTakes);
        rb.simulated = false;
    }

    public override void Drop()
    {
        base.Drop();
        rb.simulated = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
