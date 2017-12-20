using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Damageble {

    public float bulletLifeTime;
    public LayerMask whatIsTarget;

    private Rigidbody2D rb;
    private Vector2 bulletVelocity;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = bulletVelocity;
        /*Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        direction.Normalize();*/

    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(rb.velocity);
        if (bulletLifeTime < 0)
        {
            Die();
        }

        bulletLifeTime -= Time.deltaTime;
	}

    public void SetVelocity(Vector2 velocity)
    {
        bulletVelocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & whatIsTarget) != 0)
        {
            Die();
        }
    }

}
