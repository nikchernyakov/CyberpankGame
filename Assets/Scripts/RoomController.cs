using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {

    public List<Room> roomList;
    public int roomsCount = 3;

    protected virtual void Start()
    {
        foreach (Room room in roomList)
        {
            room.RegisterListener(ChangeRoom);
        }
    }

    public virtual void ChangeRoom(Room roomObject)
    {
        int roomInd = roomList.LastIndexOf(roomObject);
        UpdateRooms(roomInd);
    }

    public void UpdateRooms(int centerRoomInd)
    {
        for(int ind = 0; ind < roomList.Count; ind++)
        {
            if(ind >= centerRoomInd - roomsCount / 2 && ind <= centerRoomInd + roomsCount / 2)
            {
                roomList[ind].Show();
            }
            else
            {
                roomList[ind].Hide();
            }
        }
    }


}
