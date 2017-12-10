using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Doneble {

    public bool isPressed = true;
    public int pressingObjectsCount = 0;

    public override bool IsDone()
    {
        return isPressed;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangePress()
    {
        isPressed = !isPressed;

        UpdateDone();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        pressingObjectsCount++;

        if (pressingObjectsCount == 1)
        {
            ChangePress();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        pressingObjectsCount--;

        if(pressingObjectsCount <= 0)
        {
            pressingObjectsCount = pressingObjectsCount < 0 ? 0 : pressingObjectsCount;
            ChangePress();
        }

        
    }
}
