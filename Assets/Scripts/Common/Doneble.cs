using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DonebleEvent : UnityEvent<Doneble>
{

}

public abstract class Doneble : MonoBehaviour{

    public abstract bool IsDone();

    private DonebleEvent m_donebleEvent = new DonebleEvent();

    public void UpdateDone()
    {
        if(m_donebleEvent != null)
        {
            m_donebleEvent.Invoke(this);
        }
    }

    public void RegisterListener(UnityAction<Doneble> call)
    {
        if(m_donebleEvent != null)
            m_donebleEvent.AddListener(call);
    }

}
