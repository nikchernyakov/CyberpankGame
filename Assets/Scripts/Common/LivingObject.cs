using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingObject : Dieble {

    public int maxHP;
    private int currentHP;
    private bool alive = true;
	
	protected virtual void Start () {
        currentHP = maxHP;
	}
	
	protected virtual void Update () {
		if(currentHP <= 0)
        {
            alive = false;
        }
	}

    public int GetCurrentHP()
    {
        return currentHP;
    }

    public bool IsAlive()
    {
        return alive;
    }

    public void DecreaseHp(int decreaseCount)
    {
        currentHP = Mathf.Clamp(currentHP - decreaseCount, 0, maxHP);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.GetTagNameByEnum(TagEnum.Damageble)))
        {
            DecreaseHp(collision.GetComponent<Damageble>().damage);
        }
    }
}
