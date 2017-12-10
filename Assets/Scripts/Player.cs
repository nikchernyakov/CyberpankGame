﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingObject {

    public Robot attackRobot;
    public Robot agilityRobot;
    public Robot tankRobot;
    private Dictionary<int, Robot> robots = new Dictionary<int, Robot>();

    // For checking ground
    bool grounded = false;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    bool facingRight = false; // For checking what side is turned

    private Robot currentRobot;
    private Animator animator;

    private float move;
    private Rigidbody2D rb;

    public Transform zRotate; // Rotate's object for axis Z

    // Rotation bounds
    public float minAngle = 0;
    public float maxAngle = 0;

    private float curTimeout, angle;
    private int invert;
    private Vector3 mouse;

    private JumpHandler jumpHandler;

    void Start () {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        jumpHandler = GetComponent<JumpHandler>();

        robots.Add(attackRobot.GetRobotID(), attackRobot);
        attackRobot.gameObject.SetActive(true);

        robots.Add(agilityRobot.GetRobotID(), agilityRobot);
        agilityRobot.gameObject.SetActive(false);

        robots.Add(tankRobot.GetRobotID(), tankRobot);
        tankRobot.gameObject.SetActive(false);

        currentRobot = attackRobot;

        if (!facingRight) invert = -1; else invert = 1;
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(currentRobot.groundCheck.position, groundRadius, whatIsGround);

        move = Input.GetAxis("Horizontal");
    }

    void Update () {
        CheckChangeRobot();

        CheckMove();

        if(currentRobot.hasGun) CheckAttack();

        CheckExtra();

        if (zRotate) SetRotation();

        if (animator.GetBool(currentRobot.GetHasGunValueName()) != currentRobot.hasGun)
        {
            animator.SetBool(currentRobot.GetHasGunValueName(), currentRobot.hasGun);
            animator.SetTrigger(currentRobot.GetGunTriggerName());
        }

    }

    void ChangeRobot(int robotID)
    {
        animator.SetTrigger("ChangeRobot");
        currentRobot.Off();
        currentRobot.gameObject.SetActive(false);
        robots.TryGetValue(robotID, out currentRobot);
        currentRobot.gameObject.SetActive(true);
        animator.SetInteger("RobotID", robotID);

    }

    void CheckChangeRobot()
    {
        if (currentRobot.GetRobotID() != 0 && Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeRobot(attackRobot.GetRobotID());
        }
        else if (currentRobot.GetRobotID() != 1 && Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeRobot(agilityRobot.GetRobotID());
        }
        else if (currentRobot.GetRobotID() != 2 && Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeRobot(tankRobot.GetRobotID());
        }
    }

    void CheckAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        { 
           currentRobot.AttackSkill(transform, facingRight ? -1 : 1);
        }
    }

    void CheckExtra()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.J))
        {
            currentRobot.ExtraSkill();
        }
    }

    void CheckMove()
    {
        // Check jump
        if (grounded && jumpHandler.CheckJump())
        {
            jumpHandler.Jump(currentRobot.jumpVelocity);
        }

        // Triggers for animation
        if (move != 0)
        {
            animator.SetTrigger("RobotRun");
        } else
        {
            animator.SetTrigger("RobotIdle");
        }

        Vector2 velocityVector = new Vector2(move * currentRobot.maxSpeed, rb.velocity.y);
        rb.velocity = velocityVector;

        CheckFlip();
    }

    void CheckFlip()
    {
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
        /*if (angle == maxAngle && mouse.x < zRotate.position.x && facingRight) Flip();
        else if (angle == maxAngle && mouse.x > zRotate.position.x && !facingRight) Flip();*/
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void SetRotation()
    {
        Vector3 mousePosMain = Input.mousePosition;
        mousePosMain.z = Camera.main.transform.position.z;
        mouse = Camera.main.ScreenToWorldPoint(mousePosMain);
        Vector3 lookPos = mouse - transform.position;
        angle = Mathf.Atan2(lookPos.y, lookPos.x * invert) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        zRotate.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
