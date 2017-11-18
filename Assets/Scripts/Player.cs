using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {

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

    protected override void Start () {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        robots.Add(attackRobot.robotID, attackRobot);
        attackRobot.gameObject.SetActive(true);

        robots.Add(agilityRobot.robotID, agilityRobot);
        agilityRobot.gameObject.SetActive(false);

        robots.Add(tankRobot.robotID, tankRobot);
        tankRobot.gameObject.SetActive(false);

        currentRobot = attackRobot;

        if (!facingRight) invert = -1; else invert = 1;
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(currentRobot.groundCheck.position, groundRadius, whatIsGround);

        move = Input.GetAxis("Horizontal");
    }

    protected override void Update () {
        if (currentRobot.robotID != 0 && Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeRobot(attackRobot.robotID);   
        }
        else if (currentRobot.robotID != 1 && Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeRobot(agilityRobot.robotID);
        }
        else if (currentRobot.robotID != 2 && Input.GetKeyDown(KeyCode.Alpha3)){
            ChangeRobot(tankRobot.robotID);
        }

        CheckMove();

        if(currentRobot.hasGun) CheckAttack();

        if (zRotate) SetRotation();

        if (animator.GetBool(currentRobot.hasGunValueName) != currentRobot.hasGun)
        {
            animator.SetBool(currentRobot.hasGunValueName, currentRobot.hasGun);
            animator.SetTrigger(currentRobot.gunTriggerName);
        }

    }

    void ChangeRobot(int robotID)
    {
        animator.SetTrigger("ChangeRobot");
        currentRobot.gameObject.SetActive(false);
        robots.TryGetValue(robotID, out currentRobot);
        currentRobot.gameObject.SetActive(true);
        animator.SetInteger("RobotID", robotID);

    }

    void CheckAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        { 
           currentRobot.Attack(transform, facingRight ? -1 : 1);
        }
    }

    void CheckMove()
    {
        // Check jump
        if (grounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.velocity = new Vector2(0f, 0f);
            rb.AddForce(new Vector2(0f, currentRobot.jumpForce));
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
        /*if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();*/
        if (angle == maxAngle && mouse.x < zRotate.position.x && facingRight) Flip();
        else if (angle == maxAngle && mouse.x > zRotate.position.x && !facingRight) Flip();
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
