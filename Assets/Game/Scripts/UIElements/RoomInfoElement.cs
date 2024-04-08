using UnityEngine;

public class RoomInfoElement : MonoBehaviour
{
    private Photon.Realtime.RoomInfo _roomInfo;
    public void SetRoomInfo(Photon.Realtime.RoomInfo roomInfo)
    {
        _roomInfo = roomInfo;
    }
}
