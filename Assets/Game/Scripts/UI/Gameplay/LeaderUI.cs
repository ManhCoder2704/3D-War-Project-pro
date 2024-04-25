using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderUI : UIBase
{
    [SerializeField] private Transform _blockContainer;
    [SerializeField] private ButtonBlock _btnBlockPrefab;
    [SerializeField] private Button _nextBtn;
    [SerializeField] private Button _previousBtn;
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;
    private List<Tile> _enemyList;
    private List<Tile> _allyList;
    private List<ButtonBlock> tempList = new List<ButtonBlock>();
    // Start is called before the first frame update
    void OnEnable()
    {
        _previousBtn.interactable = false;
        _nextBtn.onClick.AddListener(ChangeToAlly);
        _previousBtn.onClick.AddListener(ChangeToEnemy);
        _enemyList = SpawnManager.Instance.EnemyTiles;
        _allyList = SpawnManager.Instance.AllyTiles;
        SpawnEnemyButton();
    }
    private void SpawnEnemyButton()
    {
        for (int i = 0; i < _enemyList.Count; i++)
        {
            ButtonBlock temp = Instantiate(_btnBlockPrefab, _blockContainer);
            temp.OnInit(_enemyList[i], true);
            tempList.Add(temp);
        }
    }
    private void ChangeToAlly()
    {
        for (int i = 0; i < tempList.Count; i++)
        {
            tempList[i].OnInit(_allyList[i], false);
        }
        _previousBtn.interactable = true;
        _nextBtn.interactable = false;
        _gridLayoutGroup.startCorner = GridLayoutGroup.Corner.LowerLeft;
    }
    private void ChangeToEnemy()
    {
        for (int i = 0; i < _enemyList.Count; i++)
        {
            tempList[i].OnInit(_enemyList[i], true);
        }
        _previousBtn.interactable = false;
        _nextBtn.interactable = true;
        _gridLayoutGroup.startCorner = GridLayoutGroup.Corner.UpperRight;

    }
}
