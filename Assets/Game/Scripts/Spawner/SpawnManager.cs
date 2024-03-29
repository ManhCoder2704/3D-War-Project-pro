using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private List<Transform> _blueSpawnPoints;
    [SerializeField] private List<Transform> _redSpawnPoint;

    internal void Spawn()
    {
        GameObject leader = Instantiate(Resources.Load("Models/Leader"), _blueSpawnPoints[(int)CharacterType.Leader - 1].position, Quaternion.identity) as GameObject;
        GameObject sniper = Instantiate(Resources.Load("Models/Sniper"), _blueSpawnPoints[(int)CharacterType.Sniper - 1].position, Quaternion.identity) as GameObject;
        GameObject carrior = Instantiate(Resources.Load("Models/Carrier"), _blueSpawnPoints[(int)CharacterType.Carrier - 1].position, Quaternion.identity) as GameObject;
        GameObject trencher = Instantiate(Resources.Load("Models/Trencher"), _blueSpawnPoints[(int)CharacterType.Trencher - 1].position, Quaternion.identity) as GameObject;
        CameraManager.Instance.SetupCamera(leader, sniper, carrior);
        GameplayManager.Instance.ChangeState(GameState.Play);
    }
}
