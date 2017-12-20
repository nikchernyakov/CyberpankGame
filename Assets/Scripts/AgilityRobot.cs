using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgilityRobot : Robot {

    public float invisibleTime;
    private float currentInvisibleTime;

    public float invisibleCooldown;
    private float currentInvisibleCooldown;

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        currentInvisibleCooldown = 0;
        currentInvisibleTime = 0;
    }

    // Update is called once per frame
    void Update () {
        CheckInvisible();
	}

    void CheckInvisible()
    {
        if (currentInvisibleTime > 0)
        {
            currentInvisibleTime -= Time.deltaTime;
        }
        else
        {
            if (!player.IsVisible())
            {
                BecomeVisible();
            }
            else
            {
                if(currentInvisibleCooldown > 0)
                    currentInvisibleCooldown -= Time.deltaTime;
            }
        }


    }

    private void BecomeInvisible()
    {
        player.ChangeState(false);
        currentInvisibleTime = invisibleTime;
    }

    private void BecomeVisible()
    {
        player.ChangeState(true);
        currentInvisibleCooldown = invisibleCooldown;
    }

    public override void ExtraSkill()
    {
        base.ExtraSkill();

        if (currentInvisibleTime > 0 || currentInvisibleCooldown > 0)
            return;

        BecomeInvisible();
    }

    public override void CancelExtraSkill()
    {
        base.CancelExtraSkill();

        BecomeVisible();
    }
}
