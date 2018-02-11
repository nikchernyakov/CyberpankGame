using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour {

    public float minSpeedValue = 0f;
    public float maxSpeedValue = 0f;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, minSpeedValue, maxSpeedValue));
	}
}
