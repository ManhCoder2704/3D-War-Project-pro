using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierState : IState
{
    public void OnEnter()
    {
        CameraManager.Instance.ChangeCamera(CharacterType.Carrier);
    }

    public void OnExecute()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameplayManager.Instance.ChangeState(GameState.Leader);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameplayManager.Instance.ChangeState(GameState.Sniper);
        }
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }
}
