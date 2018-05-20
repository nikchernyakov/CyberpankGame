using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRobot : Robot
{
    private Hackable currentTarget = null;

    public override void ExtraSkill()
    {
        base.ExtraSkill();

        if (currentTarget == null) return;

        currentTarget.Hack();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag(TagManager.GetTagNameByEnum(TagEnum.Hackble)))
        {
            currentTarget = collision.gameObject.GetComponent<Hackable>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.GetTagNameByEnum(TagEnum.Hackble)))
        {
            currentTarget = null;
        }
    }


}
