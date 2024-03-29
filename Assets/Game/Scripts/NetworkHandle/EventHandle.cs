using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class EventHandle : PhotonSingleton<EventHandle>, IOnEventCallback
{
    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code >= 200) return;
        EventCode eventCode = (EventCode)photonEvent.Code;
        object[] data = (object[])photonEvent.CustomData;
        Debug.Log("OnEvent: " + eventCode);
    }

}
