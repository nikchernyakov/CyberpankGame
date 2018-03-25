using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgilityRobot : Robot {

    public float invisibleTime;
    private float currentInvisibleTime;

    public float invisibleCooldown;
    private float currentInvisibleCooldown;

    public float invisibleAlpha;
    private VisibleState visibleState;

    public AnimationRenderer animationRenderer;

    protected override void Start()
    {
        currentInvisibleCooldown = 0;
        currentInvisibleTime = 0;
        visibleState = VisibleState.VISIBLE;
    }

    // Update is called once per frame
    void Update () {
        CheckInvisible();
	}

    void CheckInvisible()
    {
        if (IsVisible())
        {
            if (currentInvisibleCooldown > 0)
                currentInvisibleCooldown -= Time.deltaTime;
        }
        else
        {
            if (currentInvisibleTime > 0)
            {
                currentInvisibleTime -= Time.deltaTime;
            }
            else
            {
                BecomeVisible();
            }
        }

    }

    private void BecomeInvisible()
    {
        ChangeState(false);
        currentInvisibleTime = invisibleTime;
    }

    private void BecomeVisible()
    {
        ChangeState(true);
        currentInvisibleTime = 0;
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

        if (!IsVisible())
        {
            BecomeVisible();
        }
    }

    public void ChangeState(bool inVisible)
    {
        gameObject.layer += inVisible ? -1 : 1;

        Color color = animationRenderer.GetSpriteRenderer().color;
        if (inVisible)
        {
            color.a = 1;
        }
        else
        {
            color.a = invisibleAlpha;
        }
        animationRenderer.GetSpriteRenderer().color = color;

        visibleState = inVisible ? VisibleState.VISIBLE : VisibleState.INVISIBLE;
    }

    public bool IsVisible()
    {
        return visibleState == VisibleState.VISIBLE;
    }
}

public enum VisibleState { VISIBLE, INVISIBLE }
