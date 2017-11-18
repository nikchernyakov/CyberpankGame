using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {
    public int robotID;
    public float maxSpeed = 10f;
	public float jumpForce = 700f;

    public bool hasGun;
    public string hasGunValueName;
    public string gunTriggerName;
    public Bullet bullet;

    public Transform gunPoint; // Bullet born point
    public Transform groundCheck; 


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void Attack(Transform transform, int invert)
    {
        Vector3 gunPos = gunPoint.localPosition;
        gunPos.x *= invert;
        Bullet clone = Instantiate(bullet, gunPos + transform.position, Quaternion.identity) as Bullet;
    }

}
