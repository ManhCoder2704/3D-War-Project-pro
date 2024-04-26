using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBlock : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private GameObject _Icon;
    [SerializeField] private Image _btnImage;
    [SerializeField] private Color _enemyColor;
    [SerializeField] private Color _allyColor;

    private Tile _linkedTile;
    private bool _isDigged;
    private bool _isThrown;
    private bool _isEnemy;
    void Start()
    {
        _btn.onClick.AddListener(ThrowBombandDig);
    }


    private void ThrowBombandDig()
    {
        TurnOnOffIcon(true);
        this._btn.interactable = false;
        if (_isEnemy)
        {
            _linkedTile.BombDrop();
        }
        else
        {
            _linkedTile.IsDigged = !_isDigged;
            _linkedTile.OnOffBlock(false);
        }
    }
    public void OnInit(Tile tile, bool isEnemyBlock)
    {
        this._linkedTile = tile;
        this._isDigged = tile.IsDigged;
        this._isThrown = tile.IsThrownBomb;
        this._isEnemy = isEnemyBlock;
        if (isEnemyBlock)
        {
            _btnImage.color = _enemyColor;
            TurnOnOffIcon(_isThrown);
            this._btn.interactable = !_isThrown;
        }
        else
        {
            _btnImage.color = _allyColor;
            TurnOnOffIcon(_isDigged);
            this._btn.interactable = !_isDigged;

        }
    }
    public void TurnOnOffIcon(bool status)
    {
        _Icon.SetActive(status);
    }


}
