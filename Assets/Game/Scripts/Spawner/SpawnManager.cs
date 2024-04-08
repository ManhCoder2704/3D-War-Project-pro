using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private List<Transform> _blueSpawnPoints;
    [SerializeField] private List<Transform> _redSpawnPoint;
    [Header("Map Generator")]
    [SerializeField] private Transform _mapContainer;
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private float _tileSize = 4f;
    [SerializeField] private int _mapWidth = 16;
    [SerializeField] private int _mapHeight = 24;

    internal List<GameObject> _tiles = new List<GameObject>();
    public GameObject leader;
    public GameObject sniper;
    public GameObject carrior;
    public GameObject trencher;


    internal void SpawnCharacter()
    {
        leader = Instantiate(Resources.Load("Models/Leader"), _blueSpawnPoints[(int)CharacterType.Leader - 1].position, Quaternion.identity) as GameObject;
        sniper = Instantiate(Resources.Load("Models/Sniper"), _blueSpawnPoints[(int)CharacterType.Sniper - 1].position, Quaternion.identity) as GameObject;
        carrior = Instantiate(Resources.Load("Models/Carrier"), _blueSpawnPoints[(int)CharacterType.Carrier - 1].position, Quaternion.identity) as GameObject;
        trencher = Instantiate(Resources.Load("Models/Trencher"), _blueSpawnPoints[(int)CharacterType.Trencher - 1].position, Quaternion.identity) as GameObject;
        CameraManager.Instance.SetupCamera(leader, sniper, carrior);
    }

    internal void MapGenerator(Action onComplete)
    {
        StartCoroutine(MapGeneratorCO(onComplete));
    }

    private IEnumerator MapGeneratorCO(Action onComplete)
    {
        for (int z = 0; z < _mapHeight; z++)
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                GameObject tile = Instantiate(_tilePrefab, _mapContainer);
                _tiles.Add(tile);
                tile.name = $"Tile_{x}_{z}";
                tile.transform.localPosition = new Vector3(x * _tileSize, 0, z * _tileSize);
                Debug.Log($"Generating Tile {x}_{z}");
            }
            yield return null;
        }
        SpawnCharacter();
        yield return null;
        onComplete?.Invoke();
    }
}
