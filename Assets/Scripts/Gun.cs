using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public Transform gunPoint; // Bullet born point
    public Bullet bullet;
    public float bulletSpeed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Shot()
    {
        
        Bullet clone = Instantiate(bullet, gunPoint.transform.position, Quaternion.identity) as Bullet;
        clone.transform.rotation = transform.rotation;
        clone.SetDirection(gunPoint.transform.position - transform.position);
        clone.SetSpeed(bulletSpeed);
    }
}
