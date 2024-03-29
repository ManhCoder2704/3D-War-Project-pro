using UnityEngine;

public class PrepareState : IState
{
    public void OnEnter()
    {
        SpawnManager.Instance.Spawn();
    }

    public void OnExecute()
    {

    }

    public void OnExit()
    {

    }
}
