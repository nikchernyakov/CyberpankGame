using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openble : MonoBehaviour {

    public List<Doneble> donebleList;
    public bool isOpen = false;

	void Start () {
		foreach(Doneble doneble in donebleList)
        {
            doneble.RegisterListener(UpdateDone);
        }
	}

	void Update () {
		
	}

    public void ChangeOpen()
    {
        gameObject.SetActive(!isOpen);
    }

    public void UpdateDone(Doneble donebleObject)
    {
        
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

    private bool CheckDonebleList(bool needDone)
    {
        foreach (Doneble donebleElement in donebleList)
        {
            if (donebleElement.IsDone() != needDone)
                return false;
        }
        return true;
    }

}
