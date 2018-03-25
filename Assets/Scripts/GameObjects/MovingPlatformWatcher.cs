using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformWatcher : MonoBehaviour {

    public TagEnum movingGroundTag;

    private GameObject target;
    private Vector3 offset;
    private Vector3 previousPosition;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(target != null)
        {
            transform.position += target.transform.position - previousPosition;
            previousPosition = target.transform.position;
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("OnTriggerStay " + other.tag);
        if (other.tag.Equals(TagManager.GetTagNameByEnum(movingGroundTag)))
        {
            //transform.parent = other.transform;
            target = other.gameObject;
            previousPosition = target.transform.position;
            //offset = transform.position - target.transform.position;
            //Debug.Log("OnTriggerEnter");
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
    }*/

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals(TagManager.GetTagNameByEnum(movingGroundTag)))
        {
            Debug.Log("OnTriggerExit");
            //transform.parent = null;
            target = null;
        }
    }


}
