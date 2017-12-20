using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public Transform gunPoint; // Bullet born point
    public Bullet bullet;
    public float bulletSpeed;
    public Transform bulletContainer;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Shot()
    {
        
        Bullet clone = Instantiate(bullet, gunPoint.transform.position, Quaternion.identity) as Bullet;

        clone.transform.parent = bulletContainer;
        clone.transform.rotation = transform.rotation;
        Vector2 velocity = gunPoint.transform.position - transform.position;
        clone.SetVelocity(velocity * bulletSpeed);
        //clone.SetDirection();
        //clone.SetSpeed(bulletSpeed);
    }
}
