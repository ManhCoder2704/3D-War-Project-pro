using Lobby;
using UnityEngine;

public class LeaderState : IState
{
    public void OnEnter()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        CameraManager.Instance.ChangeCamera(CharacterType.Leader);
    }

    public void OnExecute()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameplayManager.Instance.ChangeState(GameState.Sniper);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameplayManager.Instance.ChangeState(GameState.Carrier);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Gameplay.UIManager.Instance.OpenUI(UIType.LeaderUI);
        }
    }

    public void OnExit()
    {
    }
}
