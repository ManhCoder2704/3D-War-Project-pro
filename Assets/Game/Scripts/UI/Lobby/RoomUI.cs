using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby
{
    public class RoomUI : UIBase
    {
        [Header("Button")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _leaveButton;

        [Header("Text")]
        [SerializeField] private TMP_Text _roomID;

        private void OnEnable()
        {
            _roomID.text = PhotonNetwork.CurrentRoom.Name;
            _startButton.interactable = (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2);
        }

        private void Start()
        {
            _startButton.onClick.AddListener(OnStart);
            _leaveButton.onClick.AddListener(OnLeave);
            Launcher.Instance.OnPlayerCountChange += OnPlayerCountChange;
            base.Init();
        }

        private void OnStart()
        {
            //GameManager.Instance.StartGame();
        }

        private void OnLeave()
        {
            Launcher.Instance.LeaveRoom();
        }

        private void OnPlayerCountChange(Photon.Realtime.Player enemyInfo, int playerCount, bool isJoin)
        {
            _startButton.interactable = (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2);
        }

        private void OnDisable()
        {
            Launcher.Instance.OnPlayerCountChange -= OnPlayerCountChange;
        }
    }
}
