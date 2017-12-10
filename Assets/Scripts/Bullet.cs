using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Damageble {

    public float bulletLifeTime;

    private Rigidbody2D rb;
    private Vector2 direction;
    private float bulletSpeed;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();

        /*Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        direction.Normalize();*/

    }
	
	// Update is called once per frame
	void Update () {
        if(bulletLifeTime < 0)
        {
            Destroy(gameObject);
        }

        if (direction == null) return;

        rb.velocity = new Vector2(direction.x * bulletSpeed, direction.y * bulletSpeed);
        bulletLifeTime -= Time.deltaTime;
	}

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
        direction.Normalize();
    }

    public void SetSpeed(float speed)
    {
        bulletSpeed = speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.CompareTag(TagManager.GetTagNameByEnum(TagEnum.Player)))
        {
            //Debug.Log("Receive Damage");
            collision.gameObject.GetComponent<Player>().ReceiveDamage(damageAmount);
        }*/
        Destroy(gameObject);
    }

}
