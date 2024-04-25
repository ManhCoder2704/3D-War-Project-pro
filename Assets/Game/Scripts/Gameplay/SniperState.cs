using Gameplay;
using UnityEngine;

public class SniperState : IState
{
    private bool _alpha1Pressed;
    private bool _alpha3Pressed;
    private bool _isShooting;
    public void OnEnter()
    {
        CameraManager.Instance.ChangeCamera(CharacterType.Sniper);
        GameplayManager.Instance.MouseVisible(false);
        UIManager.Instance.OpenUI(UIType.SniperOverlay);
    }

    private void InputHandler()
    {
        _alpha1Pressed = Input.GetKeyDown(KeyCode.Alpha1);
        _alpha3Pressed = Input.GetKeyDown(KeyCode.Alpha3);
        _isShooting = Input.GetMouseButtonDown(0);
    }

    private void ChangeState()
    {
        if (_alpha1Pressed) GameplayManager.Instance.ChangeState(GameState.Leader);

        else if (_alpha3Pressed) GameplayManager.Instance.ChangeState(GameState.Carrier);
    }

    private void Shoot()
    {
        if (_isShooting)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log($"<color=green>Sniper Shot at {hit.collider.gameObject.name}</color>");
            }
        }
    }

    public void OnExecute()
    {
        InputHandler();
        ChangeState();
        Shoot();
    }

    public void OnExit()
    {
        GameplayManager.Instance.MouseVisible(true);
    }

}
