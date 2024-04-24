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
    private bool _status;
    void Start()
    {
        _btn.onClick.AddListener(ThrowBomb);
    }

    private void ThrowBomb()
    {

    }
    public void OnInit(Tile tile, bool isEnemyBlock)
    {
        this._linkedTile = tile;
        if (isEnemyBlock)
        {
            _btnImage.color = _enemyColor;
        }
        else
        {
            _btnImage.color = _allyColor;
        }
        TurnOnOffIcon(_status);

    }
    public void TurnOnOffIcon(bool status)
    {
        this._status = !_status;
        _Icon.SetActive(status);
    }


}
