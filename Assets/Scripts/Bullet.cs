using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed;
    public float bulletLifeTime;

    private Rigidbody2D rb;
    private Vector2 direction;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        direction.Normalize();

    }
	
	// Update is called once per frame
	void Update () {
        if(bulletLifeTime < 0)
        {
            Destroy(gameObject);
        }
        rb.velocity = new Vector2(direction.x * bulletSpeed, direction.y * bulletSpeed);
        bulletLifeTime -= Time.deltaTime;
	}
}
