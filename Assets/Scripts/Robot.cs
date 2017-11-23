using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {
    public int robotID;
    public float maxSpeed = 10f;
	public float jumpForce = 700f;

    public bool hasGun;
    public string hasGunValueName;
    public string gunTriggerName;
    public Bullet bullet;

    public Transform gunPoint; // Bullet born point
    public Transform groundCheck; 

    public int GetRobotID()
    {
        return robotID;
    }

    public string GetHasGunValueName()
    {
        return hasGunValueName;
    }

    public string GetGunTriggerName()
    {
        return gunTriggerName;
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
}
