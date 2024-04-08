using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby
{
    public class RoomListUI : UIBase
    {
        [Header("Prefab")]
        [SerializeField] private RoomInfoElement _roomBoxPrefab;

        [Header("Button")]
        [SerializeField] private Button _leaveRoomButton;
        [SerializeField] private Button _quickJoinButton;

        [Header("Container")]
        [SerializeField] private Transform _roomListContainer;

        [Header("Text")]
        [SerializeField] private TMP_Text _roomCountText;

        private List<RoomInfoElement> _roomInfoElements = new List<RoomInfoElement>();

        private void OnEnable()
        {
            Launcher.Instance.OnRoomListChange += OnRoomListUpdate;
            OnRoomListUpdate(Launcher.Instance.CurrentRoomList);
        }

        private void Start()
        {
            _leaveRoomButton.onClick.AddListener(OnLeaveButtonClicked);
            //_quickJoinButton.onClick.AddListener(OnQuickJoinButtonClicked);
            base.Init();
        }

        private void OnLeaveButtonClicked()
        {
            Lobby.UIManager.Instance.OpenUI(UIType.Menu);
        }

        private void OnQuickJoinButtonClicked()
        {
            //GameManager.Instance.QuickJoin();
        }

        private void OnRoomListUpdate(List<Photon.Realtime.RoomInfo> roomList)
        {
            RemoveList();
            RefreshRoomList(roomList);
        }

        private void RefreshRoomList(List<Photon.Realtime.RoomInfo> roomList)
        {
            foreach (var r in roomList)
            {
                var roomInfoElement = Instantiate(_roomBoxPrefab, _roomListContainer);
                roomInfoElement.SetRoomInfo(r);
                _roomInfoElements.Add(roomInfoElement);
            }
        }

        private void RemoveList()
        {
            foreach (var room in _roomInfoElements)
            {
                Destroy(room.gameObject);
            }

            _roomInfoElements.Clear();
        }

        private void OnDisable()
        {
            Launcher.Instance.OnRoomListChange -= OnRoomListUpdate;
            RemoveList();
        }
    }
}
