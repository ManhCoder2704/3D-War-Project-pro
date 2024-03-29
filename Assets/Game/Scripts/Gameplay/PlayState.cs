using UnityEngine;

public class PlayState : IState
{
    public void OnEnter()
    {

    }

    public void OnExecute()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CameraManager.Instance.ChangeCamera(CharacterType.Leader);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CameraManager.Instance.ChangeCamera(CharacterType.Sniper);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CameraManager.Instance.ChangeCamera(CharacterType.Carrier);
        }
    }

    public void OnExit()
    {

    }
}
