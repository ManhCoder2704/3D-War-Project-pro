using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : PhotonSingleton<Launcher>
{
    [SerializeField] private AppSettings _appSettings;

    public Action<Photon.Realtime.Player, int, bool> OnPlayerCountChange;
    public Action<List<Photon.Realtime.RoomInfo>> OnRoomListChange;
    public Action<Photon.Realtime.Player> OnMasterClientChange;

    private List<Photon.Realtime.RoomInfo> _currentRoomList = new List<Photon.Realtime.RoomInfo>();

    public List<Photon.Realtime.RoomInfo> CurrentRoomList => _currentRoomList;

    public Action OnLeaveRoom;

    private bool _isReconnecting;

    private void Start()
    {
        ConnectToMasterServer();
    }

    public void ForceDisconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public void ConnectToMasterServer()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings(_appSettings);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        if (_isReconnecting)
        {
            _isReconnecting = false;
            ForceDisconnect();
            return;
        }
        Debug.Log("Joined Lobby");
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerName");
            //UIManager.Instance.OpenUI(UIType.Menu);
        }
        else
        {
            //UIManager.Instance.OpenUI(UIType.Profile);
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Room List Updated");
        _currentRoomList.Clear();
        Debug.Log("=======================RoomList=======================");
        foreach (var room in roomList)
        {
            Debug.Log(room.Name);
            if (!room.IsOpen || room.PlayerCount <= 0 || room.RemovedFromList) continue;
            _currentRoomList.Add(room);
        }
        OnRoomListChange?.Invoke(_currentRoomList);
    }

    public void CreateRoom(string roomID)
    {
        if (string.IsNullOrEmpty(roomID) || !PhotonNetwork.InLobby) return;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(roomID, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        //UIManager.Instance.OpenUI(UIType.Room);
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("Player Entered Room");
        OnPlayerCountChange?.Invoke(newPlayer, PhotonNetwork.CurrentRoom.PlayerCount, true);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log("Player Left Room");
        OnPlayerCountChange?.Invoke(otherPlayer, PhotonNetwork.CurrentRoom.PlayerCount, false);
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        OnMasterClientChange?.Invoke(newMasterClient);
    }

    public void LeaveRoom()
    {
        Debug.Log("Leave Room");
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();
    }

    public void LeaveRoomAndLoadScene(int level)
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LoadLevel(level);
            LeaveRoom();
        }

    }

    public override void OnLeftRoom()
    {
        OnLeaveRoom?.Invoke();
        _isReconnecting = true;
        PhotonNetwork.AutomaticallySyncScene = false;
        Debug.Log("Left Room");
        //UIManager.Instance.OpenUI(UIType.Menu);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected");
        ConnectToMasterServer();
    }
}


