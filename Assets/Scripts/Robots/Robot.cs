using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {
    public int robotID;
    public float maxSpeed = 10f;
    public float jumpVelocity = 700f;
    public float positiveVelocityYBound = 2f;

    public bool hasGun;
    public HasGunValueNameEnum hasGunValueName;
    public GunTriggerNameEnum gunTriggerName;
    public Bullet bullet;

    public Transform gunPoint; // Bullet born point
    public Transform groundCheck;

    private Vector2 robotSize;

    protected virtual void Start()
    {
        
    }

    public void SetRobotSize()
    {
        robotSize = GetComponent<CapsuleCollider2D>().size;
    }

    public Vector2 GetSize()
    {
        return robotSize;
    }

    public int GetRobotID()
    {
        return robotID;
    }

    public string GetHasGunValueName()
    {
        return TagManager.GetTagNameByEnum(hasGunValueName);
    }

    public string GetGunTriggerName()
    {
        return TagManager.GetTagNameByEnum(gunTriggerName);
    }

    public void AttackSkill(Transform transform, int invert)
    {
        Vector3 gunPos = gunPoint.localPosition;
        gunPos.x *= invert;
        Bullet clone = Instantiate(bullet, gunPos + transform.position, Quaternion.identity) as Bullet;
    }


    public virtual void ExtraSkill()
    {

    }

    public virtual void CancelExtraSkill()
    {

    }
}
