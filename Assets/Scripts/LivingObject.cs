using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingObject : MonoBehaviour {

    public int hp;
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DecreaseHp(int decreaseCount)
    {
        hp -= decreaseCount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.GetTagNameByEnum(TagEnum.Damageble)))
        {
            DecreaseHp(collision.GetComponent<Damageble>().damage);
        }
    }
}
