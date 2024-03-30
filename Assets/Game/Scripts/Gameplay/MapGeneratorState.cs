public class MapGeneratorState : IState
{
    public void OnEnter()
    {
        SpawnManager.Instance.MapGenerator(() =>
        {
            GameplayManager.Instance.ChangeState(GameState.Prepare);
        });
    }

    public void OnExecute()
    {

    }

    public void OnExit()
    {

    }
}
