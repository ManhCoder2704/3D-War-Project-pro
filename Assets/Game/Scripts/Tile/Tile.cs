using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject _block;
    [SerializeField] private Bomb _bomb;

    private bool _isDigged;
    private bool _isThrownBomb;
    private Vector3 _throwBombPos;

    public bool IsDigged { get => _isDigged; set => _isDigged = value; }
    public bool IsThrownBomb { get => _isThrownBomb; set => _isThrownBomb = value; }
    private void OnEnable()
    {
        _throwBombPos = new Vector3(_block.transform.position.x, _block.transform.position.y + 10f, _block.transform.position.z);
    }
    public void OnOffBlock(bool status)
    {
        _block.SetActive(status);
    }

    public void BombDrop()
    {
        Bomb temp = Instantiate(_bomb, _throwBombPos, Quaternion.identity);
        temp.BombDrop();
    }
}
