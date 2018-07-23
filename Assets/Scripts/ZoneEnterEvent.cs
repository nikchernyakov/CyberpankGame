using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneEnterEvent : Doneble
{
    [SerializeField]
    private ObjectShower tipShower;

    public bool isDone = false;

    public override bool IsDone()
    {
        return isDone;
    }

    protected override void ChangeDone()
    {
        isDone = (IsDone()) ? false : true;
        if (IsDone())
        {
            tipShower.UpdateDone(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "ZoneEnterEvent")
        {
            ChangeDone();
            Debug.Log("enter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Tip")
        {
            ChangeDone();
            Debug.Log("exit");
        }
    }
}
