using System.Collections;
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

    public float invisibleAlpha;
    private bool isVisible = true;

    bool facingRight = false; // For checking what side is turned

    private Robot currentRobot;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

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

    protected override void Start () {
        base.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        jumpHandler = GetComponent<JumpHandler>();

        // Add all robots to player
        // Add attack robot and set it active
        robots.Add(attackRobot.GetRobotID(), attackRobot);
        attackRobot.gameObject.SetActive(true);

        // Add agility robot
        robots.Add(agilityRobot.GetRobotID(), agilityRobot);
        agilityRobot.gameObject.SetActive(false);

        // Add tank robot
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

    protected override void Update () {
        base.Update();

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

    void ChangeRobot(int newRobotID)
    {
        Robot previousRobot = currentRobot;
        robots.TryGetValue(newRobotID, out currentRobot);
        if (currentRobot == null)
        {
            currentRobot = previousRobot;
            Debug.LogError("currentRobot == null in Player");
            return;
        }

        // Cancel all activivties for previous robot and set unactive status
        previousRobot.CancelExtraSkill();
        previousRobot.gameObject.SetActive(false);

        // Change robot to new one
        animator.SetInteger("RobotID", currentRobot.robotID);
        TriggerChangeRobot(previousRobot.robotID, newRobotID);
        currentRobot.gameObject.SetActive(true);

    }

    void TriggerChangeRobot(int robotIDfrom, int robotIDto)
    {
        /*
         * Determine what animation we must to use
         * All animations collected by order, which depends on robotIDfrom
         * Animations for certain robot stay in robotID * 2 index and +1 (because all robots has 2 animations of transform)
         * 
         * This formula help to find index in this order
         */
        int from = robotIDfrom, to = robotIDto, animationNumber;
        if (robotIDfrom < robotIDto)
            to--;

        animationNumber = from * 2 + to;

        animator.SetFloat("RobotChangeAnimationID", animationNumber);
        animator.SetTrigger("ChangeRobot");
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
            jumpHandler.Jump(currentRobot.jumpVelocity, currentRobot.positiveVelocityYBound);
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

    public void ChangeState(bool inVisible)
    {
        gameObject.layer += inVisible ? -1 : 1;

        Color color = spriteRenderer.color;
        if (inVisible)
        {
            color.a = 1;
        }
        else
        {
            color.a = invisibleAlpha;
        }
        spriteRenderer.color = color;

        isVisible = inVisible;
    }

    public bool IsVisible()
    {
        return isVisible;
    }

}
