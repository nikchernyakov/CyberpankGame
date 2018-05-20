using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script is register UpdateDone() method for event listening in all Doneble events
 * that this script contains.
 */
public abstract class DonebleChecker : MonoBehaviour {

    public List<Doneble> donebleList;

    protected virtual void Start()
    {
        foreach (Doneble doneble in donebleList)
        {
            doneble.RegisterListener(UpdateDone);
        }
    }

    /*
     * Method that is invoking when some of event was updated
     */
    public abstract void UpdateDone(Doneble donebleObject);

    /*
     * Method check all Doneble from list that they have a 'needDone' state
     */
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
