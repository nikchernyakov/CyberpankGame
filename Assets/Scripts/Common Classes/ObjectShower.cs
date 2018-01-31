using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShower : DonebleChecker {

    public Showeble target;

    public override void UpdateDone(Doneble donebleObject)
    {
        base.UpdateDone(donebleObject);

        if (CheckDonebleList(true))
        {
            ShowObject();
        }
        else
        {
            target.Hide();
        }
    }

    public void ShowObject()
    {
        target.Show();
    }
}
