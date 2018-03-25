using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonebleChecker : MonoBehaviour {

    public List<Doneble> donebleList;

    protected virtual void Start()
    {
        foreach (Doneble doneble in donebleList)
        {
            doneble.RegisterListener(UpdateDone);
        }
    }

    public virtual void UpdateDone(Doneble donebleObject)
    {
        
    }

    protected bool CheckDonebleList(bool needDone)
    {
        foreach (Doneble donebleElement in donebleList)
        {
            if (donebleElement.IsDone() != needDone)
                return false;
        }
        return true;
    }
}
