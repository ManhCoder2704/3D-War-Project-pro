using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [Header("Merc Spawn Points")]
    [SerializeField] private List<Transform> _blueSpawnPoints;
    [SerializeField] private List<Transform> _redSpawnPoint;

    [Header("Map Generator")]
    [SerializeField] private Transform _mapContainer;
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private float _tileSize = 4f;
    [SerializeField] private int _mapWidth = 16;
    [SerializeField] private int _mapHeight = 24;

    [Header("Package")]
    [SerializeField] private Transform _packageContainer;
    [SerializeField] private Package _packagePrefab;

    private List<Package> _packages = new List<Package>();
    private List<Tile> allyTiles = new List<Tile>();
    private List<Tile> enemyTiles = new List<Tile>();
    private GameObject leader;
    private GameObject sniper;
    private GameObject carrior;
    private GameObject trencher;

    internal List<Tile> AllyTiles { get => allyTiles; set => allyTiles = value; }
    internal List<Tile> EnemyTiles { get => enemyTiles; set => enemyTiles = value; }

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
        for (int z = 1; z < _mapHeight / 2; z++)
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                Tile tile = Instantiate(_tilePrefab, _mapContainer);

                tile.name = $"Tile_{x}_{z}";
                tile.transform.localPosition = new Vector3(x * _tileSize, 0, z * _tileSize);
                Debug.Log($"Generating Tile {x}_{z}");
                AllyTiles.Add(tile);

                Tile tile1 = Instantiate(_tilePrefab, _mapContainer);

                tile1.name = $"Tile_{_mapWidth - x - 1}_{_mapHeight - z - 1}";
                tile1.transform.localPosition = new Vector3((_mapWidth - x - 1) * _tileSize, 0, (_mapHeight - z - 1) * _tileSize);
                Debug.Log($"Generating Tile {_mapWidth - x - 1}_{_mapHeight - z - 1}");
                EnemyTiles.Add(tile1);
            }
            yield return null;
        }
        SpawnCharacter();
        yield return null;
        onComplete?.Invoke();
    }

    public void SpawnAirDrop()
    {
        int height = UnityEngine.Random.Range(_mapHeight / 4, _mapHeight / 2);
        int width = UnityEngine.Random.Range(0, _mapWidth);
        int index = width + height * _mapWidth;
        Package packageAlly = Instantiate(_packagePrefab, _packageContainer);
        Package packageEnemy = Instantiate(_packagePrefab, _packageContainer);

        Vector3 allyPos = enemyTiles[index].transform.position;
        packageAlly.transform.position = allyPos + Vector3.up * 50;

        Vector3 enemyPos = allyTiles[index].transform.position;
        packageEnemy.transform.position = enemyPos + Vector3.up * 50;
    }
}
