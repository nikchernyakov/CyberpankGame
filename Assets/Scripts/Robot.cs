using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {
    public float maxSpeed = 10f;
	public float jumpForce = 700f;
    public Bullet bullet;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Attack(Transform transform)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
       
        Bullet bulletInstance = Instantiate(bullet, transform.position, Quaternion.FromToRotation(transform.position, mousePosition)) as Bullet;
    }
}
