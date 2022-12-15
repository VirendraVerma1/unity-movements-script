using Photon.Realtime;
using UnityEngine;

public class RoomInfoBean : MonoBehaviour
{
    public string roomName;
    public RoomInfo roomInfo;

    public RoomInfoBean(string RoomName,RoomInfo RoomInfo)
    {
        roomName = RoomName;
        roomInfo = RoomInfo;
    }
}
