using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Room : Showeble {

    public class RoomEvent : UnityEvent<Room>
    {

    }

    private RoomEvent m_roomEvent = new RoomEvent();

    public void EnterRoom()
    {
        if (m_roomEvent != null)
        {
            m_roomEvent.Invoke(this);
        }
    }

    public void RegisterListener(UnityAction<Room> call)
    {
        if (m_roomEvent != null)
            m_roomEvent.AddListener(call);
    }
}
