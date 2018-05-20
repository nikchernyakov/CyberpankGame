using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openable : DonebleChecker{
    public bool isOpen = false;

    public void ChangeOpen()
    {
        gameObject.SetActive(!isOpen);
    }

    public override void UpdateDone(Doneble donebleObject)
    {
        base.UpdateDone(donebleObject);

        bool needChangeOpen = false;   
        if (isOpen && !donebleObject.IsDone())
        {
            needChangeOpen = true;
        }
        else if (!isOpen && donebleObject.IsDone())
        {
            
            needChangeOpen = CheckDonebleList(true);
            //Debug.Log(needChangeOpen);
        }

        if (needChangeOpen)
        {
            isOpen = !isOpen;
            ChangeOpen();
        }
    }

}
