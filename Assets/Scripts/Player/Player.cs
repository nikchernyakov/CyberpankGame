using System.Collections.Generic;
using UnityEngine;

public class Player : LivingObject {

    public AnimationRenderer animationRenderer;
    private Animator robotAnimator;
    public Robot attackRobot;
    public Robot agilityRobot;
    public Robot tankRobot;
    private readonly Dictionary<int, Robot> _robots = new Dictionary<int, Robot>();

    private GroundChecker _groundChecker;
    private FlipChecker _flipChecker;
    
    private Robot _currentRobot;

    private float _move;
    private Rigidbody2D _rigidbody;

    private JumpHandler _jumpHandler;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _jumpHandler = GetComponent<JumpHandler>();
        _groundChecker = GetComponent<GroundChecker>();
        _flipChecker = GetComponent<FlipChecker>();
        robotAnimator = animationRenderer.GetComponent<Animator>();
    }

    protected override void Start () {
        base.Start();

        foreach (Robot robot in (new Robot[] {attackRobot, agilityRobot, tankRobot}))
        {
            _robots.Add(robot.GetRobotID(), robot);
            robot.gameObject.SetActive(false);
            robot.SetColliderProperties();
        }

        _currentRobot = attackRobot;
        _currentRobot.gameObject.SetActive(true);
        
        _groundChecker.SetGroundCheck(_currentRobot.GroundCheck);
    }

    void FixedUpdate()
    {
        _move = Input.GetAxis("Horizontal");
    }

    protected override void Update () {
        base.Update();

        CheckChangeRobot();

        CheckMove();

        if(_currentRobot.hasGun) CheckAttack();

        CheckExtra();

        /*if (animationRenderer.GetBool(_currentRobot.GetHasGunValueName()) != _currentRobot.hasGun)
        {
            animationRenderer.SetBool(_currentRobot.GetHasGunValueName(), _currentRobot.hasGun);
            animationRenderer.SetTrigger(_currentRobot.GetGunTriggerName());
        }*/
    }


    void ChangeRobot(int newRobotId)
    {
        var previousRobot = _currentRobot;
        _robots.TryGetValue(newRobotId, out _currentRobot);
        if (_currentRobot == null)
        {
            _currentRobot = previousRobot;
            Debug.LogError("currentRobot == null in Player");
            return;
        }

        // Cancel all activivties for previous robot and set unactive status
        previousRobot.CancelExtraSkill();
        previousRobot.gameObject.SetActive(false);
        
        _currentRobot.gameObject.SetActive(true);

        // Change robot to new one
        animationRenderer.SetInteger("RobotID", _currentRobot.robotID);
        TriggerChangeRobot(previousRobot.robotID, newRobotId);
        
        _groundChecker.SetGroundCheck(_currentRobot.GroundCheck);
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
        if (animationRenderer.gameObject.activeSelf)
        {
            animationRenderer.SetInteger("RobotChangeAnimationID", animationNumber);
            animationRenderer.SetTrigger("ChangeRobotTrigger");
        }
    }

    void CheckChangeRobot()
    {
        if (_currentRobot.GetRobotID() != 0 && Input.GetKeyDown(KeyCode.Alpha1) && IsChangeRobotEnable(attackRobot.GetColliderSize()))
        {
            ChangeRobot(attackRobot.GetRobotID());

        }
        else if (_currentRobot.GetRobotID() != 1 && Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeRobot(agilityRobot.GetRobotID());
        }
        else if (_currentRobot.GetRobotID() != 2 && Input.GetKeyDown(KeyCode.Alpha3) && IsChangeRobotEnable(tankRobot.GetColliderSize()))
        {
            ChangeRobot(tankRobot.GetRobotID());
        }
    }

    bool IsChangeRobotEnable(Vector2 size)
    {
        RaycastHit2D upHit = Physics2D.Raycast(transform.position, Vector2.up, size.y, _groundChecker.WhatIsGround);
        /*RaycastHit2D upLeftHit = Physics2D.Raycast(transform.position + size.x * Vector3.left, Vector2.up, size.y, whatIsGround);
        RaycastHit2D upRightHit = Physics2D.Raycast(transform.position + size.x * Vector3.right, Vector2.up, size.y, whatIsGround);*/

        RaycastHit2D downHit = Physics2D.Raycast(transform.position, Vector2.down, size.y, _groundChecker.WhatIsGround);
        /*RaycastHit2D downLeftHit = Physics2D.Raycast(transform.position + size.x * Vector3.left, Vector2.up, size.y, whatIsGround);
        RaycastHit2D downRightHit = Physics2D.Raycast(transform.position + size.x * Vector3.right, Vector2.up, size.y, whatIsGround);*/
        return (upHit.collider == null /*&& upLeftHit.collider == null && upRightHit.collider == null*/)
            || (downHit.collider == null /*&& downLeftHit.collider == null && downRightHit.collider == null*/);
    }

    void CheckAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        { 
           _currentRobot.AttackSkill(transform, _flipChecker.IsFacingRight() ? -1 : 1);
        }
    }

    void CheckExtra()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.J))
        {
            _currentRobot.ExtraSkill();
        }
    }

    void CheckMove()
    {
        // Check jump
        if (_groundChecker.IsGrounded() && _jumpHandler.CheckJump())
        {
            _jumpHandler.Jump(_currentRobot.jumpVelocity, _currentRobot.positiveVelocityYBound);
        }

        // Triggers for animation
        if (_move != 0)
        {
            animationRenderer.SetTrigger("RobotRun");
        } else
        {
            animationRenderer.SetTrigger("RobotIdle");
        }

        Vector2 velocityVector = new Vector2(_move * _currentRobot.maxSpeed, _rigidbody.velocity.y);
        _rigidbody.velocity = velocityVector;

        _flipChecker.CheckFlip(_move);
    }

}
