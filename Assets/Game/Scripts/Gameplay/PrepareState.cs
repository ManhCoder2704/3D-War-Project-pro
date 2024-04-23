using UnityEngine;

public class PrepareState : IState
{
    private float _timer;
    public void OnEnter()
    {
        _timer = 3f;
    }

    public void OnExecute()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            Debug.Log($"Prepare Time: {_timer}");
        }
        else
        {
            GameplayManager.Instance.ChangeState(GameState.Leader);
        }
    }

    public void OnExit()
    {

    }
}
