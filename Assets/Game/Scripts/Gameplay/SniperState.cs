using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperState : IState
{
    public void OnEnter()
    {
        CameraManager.Instance.ChangeCamera(CharacterType.Sniper);
    }

    public void OnExecute()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameplayManager.Instance.ChangeState(GameState.Leader);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameplayManager.Instance.ChangeState(GameState.Carrier);

        }
    }

    public void OnExit()
    {

    }

}
