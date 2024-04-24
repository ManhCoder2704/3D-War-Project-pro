using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderUI : UIBase
{
    [SerializeField] private Transform _blockContainer;
    [SerializeField] private ButtonBlock _btnBlockPrefab;
    [SerializeField]
    private List<Tile> _enemyList;
    private List<Tile> _allyList;
    private List<ButtonBlock> tempList = new List<ButtonBlock>();
    // Start is called before the first frame update
    void OnEnable()
    {
        _enemyList = SpawnManager.Instance.EnemyTiles;
        _allyList = SpawnManager.Instance.AllyTiles;
        SpawnEnemyButton();
    }
    private void SpawnEnemyButton()
    {
        for (int i = _enemyList.Count - 1; i >= 0; i--)
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
    }
}
