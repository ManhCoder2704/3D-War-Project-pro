using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject block;

    private bool _isDigged;
    private bool _isThrownBomb;

    public bool IsDigged { get => _isDigged; set => _isDigged = value; }
    public bool IsThrownBomb { get => _isThrownBomb; set => _isThrownBomb = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnOffBlock(bool status)
    {
        block.SetActive(status);
    }
}
