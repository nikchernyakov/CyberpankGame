using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovingObject {

    public Robot attackRobot;
    public Robot agilityRobot;
    public Robot tankRobot;

    bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    bool facingRight = true;

    private Robot currentRobot;
    private Animator animator;

    private float move;
    private Rigidbody2D rb;

    protected override void Start () {
        currentRobot = attackRobot;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        grounded = grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        move = Input.GetAxis("Horizontal");
    }

    protected override void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetFloat("Blend", 0);
            animator.SetTrigger("ChangeRobot");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetFloat("Blend", 1);
            animator.SetTrigger("ChangeRobot");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)){
            animator.SetFloat("Blend", 2);
            animator.SetTrigger("ChangeRobot");
        }

        CheckMove();

        CheckAttack();

    }

    void CheckAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        { 
           currentRobot.Attack(this.transform);
        }
    }

    void CheckMove()
    {
        if (grounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            rb.velocity = new Vector2(0f, 0f);
            rb.AddForce(new Vector2(0f, currentRobot.jumpForce));
        }
        rb.velocity = new Vector2(move * currentRobot.maxSpeed, rb.velocity.y);

        CheckFlip();
    }

    void CheckFlip()
    {
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
