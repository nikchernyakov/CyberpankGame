using UnityEngine;

public class GroundChecker : MonoBehaviour {

	public float GroundRadius = 0.2f;
	public LayerMask WhatIsGround;
		
	private Transform _groundCheck;
	private bool _grounded = false;

	public bool IsGrounded()
	{
		return _grounded;
	}

	public void SetGroundCheck(Transform groundCheck)
	{
		_groundCheck = groundCheck;
	}

	private void FixedUpdate()
	{
		_grounded = Physics2D.OverlapCircle(_groundCheck.position, GroundRadius, WhatIsGround);
	}
}
