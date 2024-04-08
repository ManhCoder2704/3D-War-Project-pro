using UnityEngine;
using UnityEngine.UI;

namespace Lobby
{
    public class MenuUI : UIBase
    {
        [Header("Button")]
        [SerializeField] private Button _createRoom;
        [SerializeField] private Button _findRoom;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _exitButton;

        private void Start()
        {
            _createRoom.onClick.AddListener(OnCreateRoom);
            _findRoom.onClick.AddListener(OnFindRoom);
            _settingButton.onClick.AddListener(OnSetting);
            _exitButton.onClick.AddListener(OnExit);
            base.Init();
        }

        private void OnCreateRoom()
        {
            Launcher.Instance.CreateRoom(GetRoomID());
        }

        private void OnFindRoom()
        {
            UIManager.Instance.OpenUI(UIType.RoomList);
        }

        private void OnSetting()
        {
            //UIManager.Instance.OpenUI(UIType.Setting);
        }

        private void OnExit()
        {
            Application.Quit();
        }

        private string GetRoomID()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string roomID = "";
            for (int i = 0; i < 5; i++)
            {
                roomID += chars[Random.Range(0, chars.Length)];
            }
            return roomID;
        }
    }
}
