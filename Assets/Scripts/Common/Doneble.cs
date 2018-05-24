using UnityEngine;
using UnityEngine.Events;

public class DonebleEvent : UnityEvent<Doneble>
{

}

/*
 * Event that can register listener call method for this event
 * and invoke this method when Done state was updated
 */
public abstract class Doneble : MonoBehaviour{

    public abstract bool IsDone();

    private DonebleEvent m_donebleEvent = new DonebleEvent();

    public void UpdateDone()
    {
        ChangeDone();
        InvokeListeners();
    }

    protected abstract void ChangeDone();

    private void InvokeListeners()
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
