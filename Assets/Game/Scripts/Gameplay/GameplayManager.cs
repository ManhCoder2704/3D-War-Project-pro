using System.Collections.Generic;
using UnityEngine;
public class GameplayManager : Singleton<GameplayManager>
{
    private GameState _currentState = GameState.None;

    private Dictionary<GameState, IState> _states = new Dictionary<GameState, IState>();

    private IState _currentStateInstance;

    internal void ChangeState(GameState gameState)
    {
        if (_currentState == gameState) return;

        IState state = null;

        if (!_states.TryGetValue(gameState, out state))
        {
            switch (gameState)
            {
                case GameState.Prepare:
                    state = new PrepareState();
                    break;
                case GameState.Play:
                    state = new PlayState();
                    break;
            }
            _states.Add(gameState, state);
        }

        Debug.Log($"<color=green>Game State Changed to {gameState}</color>");
        _currentState = gameState;
        _currentStateInstance?.OnExit();
        _currentStateInstance = state;
        _currentStateInstance?.OnEnter();
    }

    internal GameState GetCurrentGameState() => _currentState;

    private void Start()
    {
        ChangeState(GameState.Prepare);
    }

    private void Update()
    {
        _currentStateInstance?.OnExecute();
    }
}
