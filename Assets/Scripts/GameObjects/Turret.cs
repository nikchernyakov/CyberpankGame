using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public Gun gun;
    public float shotTime = 5;

    private float shotTimeout = 0;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (shotTimeout <= 0)
        {
            gun.Shot();
            shotTimeout = shotTime;
        }
        else
        {
            shotTimeout -= Time.deltaTime;
        }
	}
}
